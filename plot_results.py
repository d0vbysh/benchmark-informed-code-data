"""Descriptive figures and a readable report, only after complete evaluation."""
import json
from pathlib import Path
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
ROOT=Path(__file__).resolve().parent
R=ROOT/'reproduced/figures'
R.mkdir(parents=True,exist_ok=True)
a=json.loads((ROOT/'data/evaluation_analysis.json').read_text(encoding='utf-8'))
plt.rcParams.update({'font.size':10,'figure.dpi':150,'savefig.dpi':180})
arms=['A','B','B_equal_budget','C'];labels=['Role only','One profile','Two profiles\ncontext omitted','Context retained']
fig,ax=plt.subplots(figsize=(8,4.5));bottom=[0.]*4
for key,color,title in [('correct_fraction','#2b8c69','Correct'),('wrong_fraction','#cf554d','Wrong'),('abstain','#b7bdc6','Abstain')]:
    y=[1-a['macro'][arm]['coverage'] if key=='abstain' else a['macro'][arm][key] for arm in arms]
    ax.bar(labels,y,bottom=bottom,label=title,color=color,width=.64)
    bottom=[x+v for x,v in zip(bottom,y)]
ax.set_ylim(0,1);ax.set_ylabel('Contract-macro fraction');ax.set_title('Held-out annotation outcomes (descriptive pilot)');ax.legend(loc='upper center',bbox_to_anchor=(.5,-.2),ncol=3,frameon=False)
fig.text(.5,.015,f"{a['stable_contexts']} stable contexts; {a['unresolved_contexts']} unresolved; {a['evaluated_contracts']} contracts. No confidence intervals.",ha='center',fontsize=9)
fig.tight_layout(rect=(0,.08,1,1));fig.savefig(R/'annotation_outcomes.png');fig.savefig(R/'annotation_outcomes.pdf');plt.close(fig)
rows=sorted(a['rows'],key=lambda r:(r['contract_id'],r['pair_id'],r['profile']))
fig,ax=plt.subplots(figsize=(9,10))
for i,r in enumerate(rows):
    ax.scatter(r['evaluation_ratios'],[i-.10,i+.10],s=22,color=['#3169a8','#ed9b40'])
ax.axvspan(1/1.05,1.05,color='#e3e3e3');ax.axvline(1,color='grey',lw=.8)
short={'aggregation_greater_equal':'Sum >= threshold','membership_matched_position_sum':'Position sum','membership_matched_sum':'Matched sum','membership_miss_count':'Miss count','strings_all':'Concat all','strings_nonempty':'Concat nonempty','strings_reverse':'Concat reverse'}
axis_labels=[short[r['contract_id']]+' '+r['pair_id'].rsplit('-',1)[1]+' / '+r['profile'].replace('_',' ') for r in rows]
ax.set_xscale('log');ax.set_yticks(range(len(rows)),axis_labels,fontsize=10)
ax.invert_yaxis();ax.set_xlabel('Baseline / proposed median elapsed time (log scale)');ax.set_title('Two fresh evaluation launches per context\nLeft of 1: proposed slower; right: proposed faster')
ax.scatter([],[],s=22,color='#3169a8',label='Launch 1');ax.scatter([],[],s=22,color='#ed9b40',label='Launch 2');ax.legend(loc='lower right');ax.grid(axis='x',alpha=.2)
fig.tight_layout();fig.savefig(R/'heldout_ratios.png');fig.savefig(R/'heldout_ratios.pdf');plt.close(fig)
