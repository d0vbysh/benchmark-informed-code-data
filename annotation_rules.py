"""Frozen descriptive annotation rules. No fitting or benchmark execution."""
import math
TAU=0.05

def time_label(ratio):
    if not math.isfinite(ratio) or ratio<=0:
        raise ValueError("Ratio must be positive and finite")
    if ratio>1+TAU: return "faster"
    if ratio<1/(1+TAU): return "slower"
    return None

def predictions(calibration_ratios, baseline_profile, target_profile):
    if len(calibration_ratios)!=2:
        raise ValueError("Exactly two calibration profiles required")
    # Equal-budget arm uses the same observations as C but discards context.
    aggregate=math.exp(sum(math.log(r) for r in calibration_ratios.values())/2)
    return {"A":"faster",
            "B":time_label(calibration_ratios[baseline_profile]),
            "B_equal_budget":time_label(aggregate),
            "C":time_label(calibration_ratios[target_profile])}

def reference_label(evaluation_launch_ratios):
    if len(evaluation_launch_ratios)!=2:
        raise ValueError("Two fresh evaluation launches required")
    labels=[time_label(r) for r in evaluation_launch_ratios]
    return labels[0] if labels[0] is not None and labels[0]==labels[1] else None

def score(predicted, truth):
    """Correct, wrong, abstain, utility. Undefined reference is not scored."""
    if truth not in ("faster","slower"):
        raise ValueError("Ambiguous reference must be counted separately")
    if predicted is None:
        return dict(correct=0,wrong=0,abstain=1,utility=0)
    if predicted not in ("faster","slower"):
        raise ValueError("Unknown prediction")
    correct=int(predicted==truth)
    return dict(correct=correct,wrong=1-correct,abstain=0,utility=2*correct-1)
