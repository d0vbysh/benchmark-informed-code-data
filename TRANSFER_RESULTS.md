# Benchmark-informed generator adaptation: three training seeds

## Design and endpoint

The study compares conventional adaptation with benchmark-informed adaptation of the same pinned Qwen2.5-Coder-7B-Instruct checkpoint. This is study-specific LoRA fine-tuning of an already instruction-tuned model, not training a foundation model from scratch. Both arms use the same 30 training records, code and prompts, 45 optimizer steps and 18,198 supervised tokens. Nine target labels differ. The benchmark-informed targets are derived from calibration measurements through the workload-dependent annotation rule; held-out evaluation labels are not used for training. The integration is implemented through training targets, without a new loss function.

Each seed produces one record for each of 12 held-out contracts and two workload profiles: 24 planned records per arm, with no best-of selection or replacement. A fully usable record must pass strict JSON parsing, raw compilation, functional tests, requested-mechanism review, and an independent performance-label check. Let R be median baseline time divided by median candidate time. Two fresh launches must both give R > 1.05 for faster, or both give R < 1/1.05 for slower; otherwise the reference is unresolved and does not count as a primary success. Five timing iterations per launch are retained, without outlier removal, after three warmup iterations. Denominators include every planned generation.

## Results

| Training seed | Adaptation | Compiled | Functional | Admitted | Correct and fully usable | Usable yield |
|---|---|---:|---:|---:|---:|---:|
| 20261005 | Conventional | 16/24 | 14/24 | 10/24 | 6/24 | 25.0% |
| 20261005 | Benchmark-informed | 17/24 | 14/24 | 10/24 | 8/24 | 33.3% |
| 20261006 | Conventional | 12/24 | 10/24 | 10/24 | 6/24 | 25.0% |
| 20261006 | Benchmark-informed | 12/24 | 10/24 | 10/24 | 7/24 | 29.2% |
| 20261007 | Conventional | 15/24 | 12/24 | 11/24 | 7/24 | 29.2% |
| 20261007 | Benchmark-informed | 15/24 | 13/24 | 12/24 | 8/24 | 33.3% |


Within-seed differences in usable yield (benchmark-informed minus conventional): **20261005: +8.33 pp, 20261006: +4.17 pp, 20261007: +4.17 pp**. The mean difference is **+5.56 percentage points**, with a range of **+4.17 to +8.33**. Benchmark-informed adaptation improved fully usable-record yield in each of the three tested training seeds.

Descriptive totals are 19/72 (26.4%) fully usable records for conventional adaptation and 23/72 (31.9%) for benchmark-informed adaptation. These totals summarize repeated generation slots, not 72 independent test tasks. The functional-correctness totals are 36/72 and 37/72, respectively. A gain in label reliability should not be presented as an equivalent gain in functional code correctness.

Label accuracy conditional on admission and a stable timing reference is:

- 20261005: conventional 6/10; benchmark-informed 8/10.
- 20261006: conventional 6/10; benchmark-informed 7/10.
- 20261007: conventional 7/11; benchmark-informed 8/12.

These conditional fractions use smaller, sometimes different admitted subsets and must not replace the all-slot primary endpoint. They help locate the effect in the pipeline.

Examining matched slots separates the additional usable records into two sources. On slots with a stable reference for both arms, the net gains are +2, +1 and 0 across the three seeds. Differences in which records reach that stage contribute 0, 0 and +1. This decomposition compares matched task slots; their generated source code is not necessarily identical. The matched-square-sum/no-hit task is a recurring regression: benchmark-informed adaptation predicts slower although both launches show faster in each seed. These regressions remain in the reported totals.

The unadapted checkpoint fails strict JSON format in all 24 slots in each run because of Markdown fences. Its outputs are byte-identical across the three runs. Fence-only recovery is retained solely as a secondary compilation diagnostic; unadapted functional correctness was not assessed. The primary zero usable yield for this arm is therefore a format-gated result, not evidence that none of its code can be correct.

## Scope and limitations

All three seeds are reported. Training seeds are independent initialization/order settings, not successive improvements in a learning curve. The same 12 contracts and paired profiles recur across seeds; timing iterations, launches, and seeds do not create additional independent problem definitions. The evaluation changes contracts within familiar mechanism families and uses n=128 profiles; it does not establish transfer to arbitrary code, industrial load tests or other languages, model sizes, CPUs or workload distributions. Source review was not blinded. Functional test passing is bounded evidence, not a proof for every input. The weights were not reloaded locally; tensor fingerprints are taken from execution logs, while archive integrity is verified directly.

The previously chosen practical criterion is a gain of at least 10 percentage points in fully usable yield with a functional-yield drop of no more than 5 points. Per-seed outcomes are {20261005: False, 20261006: False, 20261007: False}. This is an author-selected practical heuristic, not a statistical significance test. No population-level confidence interval or significance claim is made from three seeds and 12 repeated contracts. Training a separate downstream performance-analysis model on the generated records has not been evaluated.

## Evidence and reproduction

The three experiment folders retain original archives, issued packages, raw code, compilation diagnostics, functional test results, source decisions, job definitions, raw timing reports and per-record outcomes. Across the runs, 252 benchmark jobs and 1260 raw timing values underpin the comparison. Per-seed analysis verifies frozen artifact hashes, executed inputs and raw reports. The accompanying seed_summary.csv and summary.json provide the exact numerators and denominators used here. No interpolated or fabricated observations are included.
