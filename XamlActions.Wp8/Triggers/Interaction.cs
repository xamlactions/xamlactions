using System;
using System.Diagnostics;
using System.Windows;

namespace XamlActions.Triggers {
    public static class Interaction {
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached(
            "Triggers",
            typeof (TriggersCollection),
            typeof (Interaction),
            null);

        public static TriggersCollection GetTriggers(DependencyObject obj) {
            var triggersCollection = obj.GetValue(TriggersProperty) as TriggersCollection;
            if (triggersCollection == null) {
                triggersCollection = new TriggersCollection();
                SetTriggers(obj, triggersCollection);
            }
            return triggersCollection;
        }

        public static void SetTriggers(DependencyObject obj, TriggersCollection value) {
            obj.SetValue(TriggersProperty, value);
            var element = obj as FrameworkElement;
            if (element == null) {
                throw new ArgumentException("Interactions need to be attached to a FrameworkElement");
            }
            value.SetDependencyObject(element);
        }
    }
}