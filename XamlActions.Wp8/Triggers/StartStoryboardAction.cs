using System.Windows;
using System.Windows.Media.Animation;

namespace XamlActions.Triggers {
    public class StartStoryboardAction : TriggerAction {

        public StartStoryboardAction() {
            
        }

        public Storyboard Storyboard {
            get { return (Storyboard) GetValue(StoryboardProperty); }
            set { SetValue(StoryboardProperty, value); }
        }

        public static readonly DependencyProperty StoryboardProperty =
            DependencyProperty.Register("Storyboard",
                                        typeof (Storyboard),
                                        typeof (StartStoryboardAction),
                                        null);

        public override void StartAction() {
            if (Storyboard == null) return;
            Storyboard.Begin();
        }
    }
}