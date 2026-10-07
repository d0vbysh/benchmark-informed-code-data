"""Numerical replay of the saved transfer experiments; no generated code is executed."""
import hashlib,json,math,statistics
from pathlib import Path
R=Path(__file__).resolve().parent
def read(p):return json.loads(p.read_text(encoding='utf-8-sig'))
for n,h in read(R/'SHA256SUMS.json').items():assert hashlib.sha256((R/n).read_bytes()).hexdigest()==h,n
def label(r):return 'faster' if r>1.05 else 'slower' if r<1/1.05 else None
totals={'control':0,'benchmark':0}
for seed in (20261005,20261006,20261007):
    P=R/'transfer'/str(seed); admission=read(P/'admission.json'); archived=read(P/'evaluation_analysis.json')
    jobs=read(P/'evaluation_jobs.json'); observations={}
    for j in jobs:
        B=P/'jobs'/j['job_id'];o=read(B/'outcome.json');report=read(B/'report.json')['Benchmarks'][0]
        assert o['status']=='pass'
        values=report['Statistics']['OriginalValues']
        assert len(values)==5 and all(math.isfinite(v) and v>0 for v in values)
        assert values==o['time_ns'] and statistics.median(values)==o['median_ns']
        assert report['Memory']['BytesAllocatedPerOperation']==o['allocated_bytes']
        report_hash=hashlib.sha256((B/'report.json').read_bytes()).hexdigest()
        assert report_hash==next(v for k,v in o['raw_hashes'].items() if k.endswith('-report-full.json'))
        for file in ['input.json','job.json']:
            assert hashlib.sha256((B/file).read_bytes()).hexdigest()==o['raw_hashes'][file]
        execution=read(B/'job.json');meta=read(B/'input.json')
        assert all(execution[k]==v for k,v in j.items())
        assert meta['input']==j['input']['input'] and meta['actual']==execution['expected']
        observations[(j['attempt_id'],j['launch'])]=o['median_ns']
    counts={a:0 for a in ['base','control','benchmark']}
    for pair in admission['pairs']:
        pid=pair['pair_id'];correct=False
        if pair['admitted']:
            ratios=[observations[(pid+'-baseline',l)]/observations[(pid+'-candidate',l)] for l in [1,2]]
            ls=[label(x) for x in ratios];truth=ls[0] if ls[0] is not None and ls[0]==ls[1] else None
            correct=truth is not None and truth==pair['time_label']
            row=next(r for r in archived['records'] if r['pair_id']==pid)
            assert ratios==[row['ratio_l1'],row['ratio_l2']] and truth==row['reference_label']
        counts[pair['arm']]+=correct
    for arm,n in counts.items():
        assert n==archived['summary'][arm].get('correct',0)
        assert sum(p['arm']==arm for p in admission['pairs'])==24
        if arm in totals:totals[arm]+=n
    print(seed,counts)
assert totals=={'control':19,'benchmark':23}
print('PASS: control 19/72, benchmark-informed 23/72; all three seeds reproduced.')
