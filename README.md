# Benchmark-informed code data

Evidence, datasets, and reproduction scripts for **Benchmark-Informed Adaptation for Synthetic Code-Performance Data**.

Authors: Artur Dovbysh and Vladyslav Alieksieiev, Lviv Polytechnic National University.

Repository: [d0vbysh/benchmark-informed-code-data](https://github.com/d0vbysh/benchmark-informed-code-data).

## What the experiment shows

Quality004 admits 15 of 36 generated C# pairs (7 contracts). A 60-job calibration phase freezes annotations before 120 evaluation jobs on changed inputs. Across 30 stable contexts, retaining profile identity gives 30 correct labels; each context-free policy gives 21. The equal-budget comparison uses the same two calibration observations, either aggregated or indexed by profile. Improvement is concentrated in membership workloads.

This is a descriptive diagnostic pilot. There is no population effect confidence interval, evaluation on unseen code, or student-training gain. 100% is conditional on admitted, stable contexts, not all generated data. Admission retains only 30/72 planned contexts. The archived admission file's `timing_status: not_started` is its original pre-measurement state, not the current campaign status.

## Reproduce the article numbers

Requirements: Python 3.9+ standard library. No model, GPU, .NET installation or pip installation is required for replay.

```sh
python reproduce.py
```

The script checks the export hashes, frozen rules and annotations, all included raw files, the 5 timing values and allocations in each raw BenchmarkDotNet report, all frozen predictions, evaluation labels, pooled scores and contract-weighted scores. It writes `reproduced/summary.json` and fails on a mismatch. Run from any working directory; paths are relative to the script.

Optional figure regeneration (Matplotlib required):

```sh
python -m pip install -r requirements-figures.txt
python plot_results.py
```

Outputs are written under `reproduced/figures/`. Figures are also supplied under `figures/`.

## Contents

- `data/generation/`: actual teacher responses, generation configuration and adapter-verification metadata for all three batches. No model weights.
- `data/generated_records.json`: projected prompts, identities, contracts and result metadata for all 72 attempts; token-ID arrays omitted. Original responses remain separate.
- `sources/<attempt_id>/`: original code, mechanically normalized code and normalization diff. Both passing and rejected attempts are retained where source was available.
- `data/admission.json`: all gates and reasons; not a proof of correctness over all inputs.
- `data/contracts.json`, `evaluation_plan.json`: predefined contracts, profiles and decision protocol.
- `data/annotations_frozen.json`: predictions fixed after calibration, before evaluation.
- `data/jobs/<job_id>/`: unchanged job outcomes, inputs, executed job descriptions and raw BenchmarkDotNet JSON reports.
- `data/evaluation_analysis.json`: archived scores and all 30 contextual records.
- `examples/records.jsonl`: the two complete archived records projected compactly in the article, with exact original fields and precision.
- `examples/matched_sum_bad.cs`, `matched_sum_good.cs`: code behind those records.
- `harness/`: original direct-call measurement C# source and project. Not executed by replay.
- `annotation_rules.py`: byte-identical frozen policy and reference-label rules.
- `SHA256SUMS.json`: export checksum inventory. `EXPORT_NOTES.json` documents exclusions and projections.

## Reading the example

`quality004-membership_matched_sum-r1` sums left-array values occurring in the right array, including duplicate left positions. Ratios are baseline median time divided by proposed median time. Below 1 means the proposed code is slower. The first-hit context has ratios approximately 0.064 and 0.073; no-hit has 4.844 and 5.054. The proposed hash-set method allocates 2,248 more bytes per operation in both contexts. Timing and allocation therefore remain separate claims.

The 5% reciprocal band is an abstention rule, not a confidence interval. Reference labels require two fresh evaluation launches agreeing outside the band. The two phases use the same programs; only inputs and launches are held out.

## Measurement scope and exclusions

Recorded environment: .NET 8.0.31, BenchmarkDotNet 0.14.0, Windows 11, Intel Core i5-12400F. Three warmups, five measurements, one process per job, outlier removal disabled. Two evaluation launches per role/profile. Individual BDN reports list CPU as unknown; `data/environment.json` records the separate registry check. Workstation load and drift were not controlled.

The original `outcome.json` files retain historical hashes for console logs and invocation commands omitted from this portable package. Those omissions are enumerated explicitly; they are not silently treated as verified. Historical freeze metadata also refers to a compiled DLL that is not distributed. `reproduce.py` checks the included measurement evidence; it does not re-audit excluded logs, launch overlap or freeze timestamps independently. The original audit is supplied as historical evidence.

To obtain new timings, build the harness with the pinned .NET runtime and BenchmarkDotNet, then execute it in a new output directory using its `BQM_JOB` and `BQM_META` inputs. This is a new experiment and will not necessarily reproduce identical timings. This package is designed for numerical replay of the published comparison, not one-command reconstruction of the teacher or automatic benchmark execution. Do not overwrite the archived data.

## Publication and reuse

No repository license has been selected on behalf of the authors. Before public deposit, the authors should choose appropriate code/data licensing and confirm redistribution permissions. No third-party paper PDFs, model weights, local build caches or authentication files are included. The journal manuscript and its publisher class are supplied separately, not deposited here as a preprint.

OpenAI Codex assisted with manuscript and packaging work. Scientific values in this bundle originate from the archived executable experiment, and candidate code originates from the recorded Qwen generation. Human author review of the package and manuscript remains required before submission.


## Generator transfer experiment (added 2026-10-06)

Run `python reproduce_transfer.py` from this repository root to recompute the three-seed usable-record counts from saved raw BenchmarkDotNet timing reports. It checks exported hashes, original report digests, input records and medians, then compares reconstructed labels with the archived analysis. It does not execute generated C# or rerun training. All three seeds and all planned records are included. See TRANSFER_RESULTS.md and transfer/seed_summary.csv.

The transfer directory includes training records, held-out prompts, raw source, compiler/test outcomes, admission decisions, executed job inputs and full timing reports. Report filenames are shortened to report.json while their bytes and original digests are retained. Original console logs and command records are omitted from this public export because they contain machine-local paths; their hashes remain in outcome.json for provenance. Full originals remain in the local research archive. Model weights, adapters and local caches are not included. The issued Colab packages are preserved separately in the local PhD root; this bundle claims numerical replay, not complete GPU-training reproduction.

Training seeds repeat the same 12 held-out contracts. The pooled 19/72 versus 23/72 comparison is descriptive and must not be treated as 72 independent tasks. The study found modest positive differences in all three seeds but did not meet the preselected +10 percentage-point target.
