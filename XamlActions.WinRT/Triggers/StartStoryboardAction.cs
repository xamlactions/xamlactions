using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace XamlActions.Triggers {
    public class StartStoryboardAction : TriggerAction {
        public Storyboard Storyboard {
            get { return (Storyboard) GetValue(StoryboardProperty); }
            set { SetValue(StoryboardProperty, value); }
        }

        public static readonly DependencyProperty StoryboardProperty =
            DependencyProperty.Register("Storyboard",
                                        typeof (Storyboard),
                                        typeof (StartStoryboardAction),
                                        new PropertyMetadata(0));

        public override void StartAction() {
            if (Storyboard == null) return;
            Storyboard.Begin();
        }
    }
}