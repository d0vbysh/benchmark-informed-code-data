"""Replay saved measurements; no model, benchmark execution or third-party packages."""
import collections,hashlib,json,math,statistics
from pathlib import Path
import annotation_rules as rules
R=Path(__file__).resolve().parent;D=R/'data'
def read(p):return json.loads(p.read_text(encoding='utf-8-sig'))
def sha(p):return hashlib.sha256(p.read_bytes()).hexdigest()
def check(condition,message):
    if not condition:raise ValueError(message)
for rel,digest in read(R/'SHA256SUMS.json').items():check(sha(R/rel)==digest,'Hash mismatch: '+rel)
frozen=read(D/'annotations_frozen.json')
check(sha(D/'annotations_frozen.json')==(D/'annotations_frozen.sha256').read_text().strip(),'Frozen predictions changed')
check(sha(R/'annotation_rules.py')==frozen['annotation_rules_sha256'],'Frozen rule changed')
omitted=set(read(R/'EXPORT_NOTES.json')['omitted_historical_raw_files'])
outcomes={};counts=collections.Counter()
for phase in ['calibration','evaluation']:
    for job in read(D/(phase+'_jobs.json')):
        folder=D/'jobs'/job['job_id'];o=read(folder/'outcome.json')
        check(o['status']=='pass' and o['job_id']==job['job_id'],'Job identity/status mismatch')
        for rel,digest in o['raw_hashes'].items():
            normalized=rel.replace('\\','/');p=folder/normalized
            if p.exists():check(sha(p)==digest,'Raw hash mismatch: '+str(p))
            else:check(job['job_id']+'/'+normalized in omitted,'Unexpected missing raw file')
        report=read(next(folder.glob('bdn/results/*full.json')))['Benchmarks'][0]
        check(report['Statistics']['OriginalValues']==o['time_ns'],'Timing values differ from raw BDN report')
        check(len(o['time_ns'])==5 and all(math.isfinite(t) and t>0 for t in o['time_ns']),'Invalid timing sample')
        check(statistics.median(o['time_ns'])==o['median_ns'],'Median mismatch')
        check(report['Memory']['BytesAllocatedPerOperation']==o['allocated_bytes'],'Allocation mismatch')
        if phase=='calibration':
            check(sha(folder/'outcome.json')==frozen['calibration_outcome_hashes'][job['job_id']],'Calibration provenance mismatch')
        outcomes[(phase,job['pair_id'],job['profile'],job['launch'],job['role'])]=o
        counts[phase]+=1
contracts={c['id']:c for c in read(D/'contracts.json')};rows=[]
for row in frozen['rows']:
    pair=row['pair_id'];profile=row['profile'];contract=contracts[row['contract_id']]
    cal={}
    for z in contract['profiles']:
        cal[z]=outcomes[('calibration',pair,z,1,'bad')]['median_ns']/outcomes[('calibration',pair,z,1,'good')]['median_ns']
    check(cal==row['calibration_ratios'],'Calibration ratio mismatch')
    pred=rules.predictions(cal,contract['baseline_profile'],profile)
    check(pred==row['predictions'],'Frozen prediction mismatch')
    ratios=[];alloc=[]
    for launch in [1,2]:
        b=outcomes[('evaluation',pair,profile,launch,'bad')];g=outcomes[('evaluation',pair,profile,launch,'good')]
        ratios.append(b['median_ns']/g['median_ns'])
        alloc.append({'launch':launch,'bad_bytes':b['allocated_bytes'],'good_bytes':g['allocated_bytes'],'good_minus_bad_bytes':g['allocated_bytes']-b['allocated_bytes']})
    truth=rules.reference_label(ratios)
    rows.append({**row,'evaluation_ratios':ratios,'truth':truth,'allocations':alloc,'scores':{a:rules.score(p,truth) for a,p in pred.items()} if truth else None})
archived=read(D/'evaluation_analysis.json')
check(rows==archived['rows'],'Reconstructed evaluation records differ')
stable=[r for r in rows if r['truth']];arms=['A','B','B_equal_budget','C']
def metrics(rs,arm):
    n=len(rs);c=sum(r['scores'][arm]['correct'] for r in rs);w=sum(r['scores'][arm]['wrong'] for r in rs)
    return {'n':n,'correct':c,'wrong':w,'abstain':n-c-w,'correct_fraction':c/n,'wrong_fraction':w/n,'coverage':(c+w)/n,'selective_error':w/(c+w) if c+w else None,'utility':(c-w)/n}
pooled={a:metrics(stable,a) for a in arms};by_contract={}
for k in sorted({r['contract_id'] for r in stable}):by_contract[k]={a:metrics([r for r in stable if r['contract_id']==k],a) for a in arms}
check(pooled==archived['pooled'],'Pooled scores differ')
check(by_contract==archived['per_contract'],'Contract scores differ')
macro={a:{k:statistics.mean(v[a][k] for v in by_contract.values()) for k in ['correct_fraction','wrong_fraction','coverage','utility']} for a in arms}
for a in arms:
    for k,value in macro[a].items():check(value==archived['macro'][a][k],'Macro scores differ')
delta=macro['C']['utility']-macro['B_equal_budget']['utility']
check(delta==archived['primary_delta_utility'],'Utility difference mismatch')
check(len(stable)==30 and counts=={'calibration':60,'evaluation':120},'Unexpected campaign size')
result={'status':'PASS','verified_jobs':dict(counts),'stable_contexts':len(stable),'unresolved':len(rows)-len(stable),'pooled':pooled,'macro':macro,'delta_utility':delta,'scope':'Replay of stored measurements; not a new timing run, independent audit of historical logs, or downstream training experiment.'}
out=R/'reproduced';out.mkdir(exist_ok=True)
(out/'summary.json').write_text(json.dumps(result,indent=2)+'\n',encoding='utf-8')
print(json.dumps(result,indent=2))
