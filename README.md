# [By Design] Unity-Bug-Report-Playable-IN-42250

**Unity has stated that this was intentional by design.**
> even though the implementation exposes a limitation, it is expected behavior.

## About this issue

The paused animation job continues to execute the `ProcessAnimation` and the `ProcessRootMotion` methods.

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode.
3. Observer the "PlayState" and "Counter" text in the Game view.

Expected result: The "Counter" should not increase.

Actual result: The "Counter" is increasing.
