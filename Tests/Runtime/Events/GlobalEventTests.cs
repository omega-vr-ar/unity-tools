using NUnit.Framework;
using Omega.Tools.Experimental.Event;
using Omega.Tools.Experimental.Event.Internals;
using Omega.Tools.Experimental.Events.Attributes;
using UnityEngine.SceneManagement;

namespace Omega.Tools.Experimental.Events.Tests
{
    public class GlobalEventTests
    {
        [Test]
        [MaxTime(2500)]
        /*
         * По хорошему нужно использовать UnityTest, но по каким то причинам, при переключении сцен UnityTest перестают
         * корректно работать
         */
        public void EventManagerDispatcherShouldNotChangeEventManagerWhenSceneWereSwitchedTest()
        {
            int handlerInvocationCount = 0;

            var initialScene = SceneManager.CreateScene("initial",
                new CreateSceneParameters {localPhysicsMode = LocalPhysicsMode.None});
            initialScene.name =
                $"Initial scene for {nameof(EventManagerDispatcherShouldNotChangeEventManagerWhenSceneWereSwitchedTest)}";
            SceneManager.SetActiveScene(initialScene);

            EventAggregator.AddHandler<TestGlobalWithAttributeEvent>(_ => handlerInvocationCount++);
            EventAggregator.Event<TestGlobalWithAttributeEvent>(default);

            Assert.AreEqual(1, handlerInvocationCount);

            var secondScene = SceneManager.CreateScene("second",
                new CreateSceneParameters {localPhysicsMode = LocalPhysicsMode.None});
            secondScene.name =
                $"Second scene for {nameof(EventManagerDispatcherShouldNotChangeEventManagerWhenSceneWereSwitchedTest)}";
            SceneManager.SetActiveScene(secondScene);

            EventAggregator.Event<TestGlobalWithAttributeEvent>(default);

            Assert.AreEqual(2, handlerInvocationCount);
        }

        [SetUp]
        [TearDown]
        public void ResetEventMangers()
        {
            EventManagerDispatcher<TestGlobalWithAttributeEvent>.RemoveEventManagerInternal();
        }

        [EventCoverage(EventCoverage.Global)]
        private struct TestGlobalWithAttributeEvent
        {
        }
    }
}