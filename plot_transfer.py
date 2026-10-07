"""Optional manuscript figure regeneration; requires matplotlib."""
import json,os
from pathlib import Path
R=Path(__file__).resolve().parent
os.environ.setdefault('MPLCONFIGDIR',str(R/'.mplconfig'))
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
seeds=[20261005,20261006,20261007]
data=[json.loads((R/'transfer'/str(s)/'evaluation_analysis.json').read_text()) for s in seeds]
fig,ax=plt.subplots(figsize=(7,3.7))
for arm,offset,color,label in [('control',-.19,'#737D8C','Conventional adaptation'),('benchmark',.19,'#177C83','Benchmark-informed adaptation')]:
    ns=[x['summary'][arm]['correct'] for x in data]
    bars=ax.bar([i+offset for i in range(3)],[100*n/24 for n in ns],.36,color=color,label=label)
    for b,n in zip(bars,ns):ax.text(b.get_x()+b.get_width()/2,b.get_height()+2,f'{n}/24',ha='center',fontsize=11)
ax.set(ylim=(0,100),xticks=range(3),xticklabels=[str(s) for s in seeds],xlabel='Training seed',ylabel='Fully usable records (%)')
ax.spines[['top','right']].set_visible(False);ax.set_axisbelow(True);ax.grid(axis='y',alpha=.2)
fig.legend(*ax.get_legend_handles_labels(),loc='lower center',ncol=2,frameon=False,fontsize=10)
fig.tight_layout(rect=(0,.1,1,1));fig.savefig(R/'figures/three_seed_comparison.pdf')
