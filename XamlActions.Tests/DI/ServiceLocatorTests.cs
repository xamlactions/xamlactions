using NUnit.Framework;
using XamlActions.DI;

namespace XamlActions.Tests.DI {
    public class ServiceLocatorTests {

        [SetUp]
        public void SetUp() {
            ServiceLocator.Default = new ServiceLocator();
        }

        [Test]
        public void Can_register_function() {
            ServiceLocator.Default.Register<IFoo>(() => new Foo());
            Assert.IsInstanceOf<Foo>(ServiceLocator.Default.Resolve<IFoo>());
        }

        [Test]
        public void Can_register_singleton() {
            ServiceLocator.Default.Register<IFoo>(new Foo());
            Assert.IsInstanceOf<Foo>(ServiceLocator.Default.Resolve<IFoo>());
        }

        [Test]
        public void Can_resolve_concrete_class() {
            Assert.IsInstanceOf<Foo>(ServiceLocator.Default.Resolve<Foo>());
        }

        [Test]
        public void Can_resolve_concrete_class_with_dependencies() {
            ServiceLocator.Default.Register<IFoo>(new Foo());
            Assert.IsInstanceOf<Bar>(ServiceLocator.Default.Resolve<Bar>());
        }

        public interface IFoo {
             
        }

        public class Foo : IFoo {
            
        }

        public class Bar {
            public Bar(IFoo foo) {
                
            }
        }
    }
}
