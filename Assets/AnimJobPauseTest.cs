using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

// About this issue:
// The paused animation job continues to execute the ProcessAnimation and the ProcessRootMotion methods.
// 
// How to reproduce:
// 1. Open the "SampleScene".
// 2. Enter Play mode.
// 3. Observer the "PlayState" and "Counter" text in the Game view.
// Expected result: The "Counter" should not increase.
// Actual result: The "Counter" is increasing.

public struct MyAnimJob : IAnimationJob
{
    public int counter;

    public void ProcessAnimation(AnimationStream stream)
    {
        counter++;
    }

    public void ProcessRootMotion(AnimationStream stream)
    {
    }
}


[RequireComponent(typeof(Animator))]
public class AnimJobPauseTest : MonoBehaviour
{
    public Text playStateText;
    public Text counterText;

    private Animator _animator;
    private PlayableGraph _graph;
    private AnimationScriptPlayable _asp;

    private IEnumerator Start()
    {
        _animator = GetComponent<Animator>();
        _graph = PlayableGraph.Create(GetType().Name);
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        _asp = AnimationScriptPlayable.Create(_graph, new MyAnimJob());

        var animOutput = AnimationPlayableOutput.Create(_graph, "Animation", _animator);
        animOutput.SetSourcePlayable(_asp);

        _graph.Play();

        yield return null;
        yield return null;

        _asp.Pause();
    }

    private void LateUpdate()
    {
        playStateText.text = $"PlayState: {_asp.GetPlayState()}";
        counterText.text = $"Counter: {_asp.GetJobData<MyAnimJob>().counter}";
    }

    private void OnDestroy()
    {
        if (_graph.IsValid())
        {
            _graph.Destroy();
        }
    }
}