using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XamlActions.Tests {
    public class MediatorTests {

        private Mediator _mediator;
        private int _calledA, _calledB;

        [SetUp]
        public void SetUp() {
            _mediator = new Mediator();
            _calledA = _calledB = 0;
        }
        [Test]
        public void Should_register_msgA() {
            _mediator.Subscribe<MsgA>(Call);
            _mediator.Publish(new MsgA());
            Assert.AreEqual(1, _calledA);
        }

        [Test]
        public void Should_register_msgA_and_msgB() {
            _mediator.Subscribe<MsgA>(Call);
            _mediator.Publish(new MsgA());
            Assert.AreEqual(1, _calledA);
            Assert.AreEqual(0, _calledB);

            _mediator.Subscribe<MsgB>(Call);
            _mediator.Publish(new MsgB());
            Assert.AreEqual(1, _calledA);
            Assert.AreEqual(1, _calledB);
        }

        [Test]
        public void Can_call_msgA_many_times() {
            _mediator.Subscribe<MsgA>(Call);
            _mediator.Publish(new MsgA());
            _mediator.Publish(new MsgA());
            _mediator.Publish(new MsgA());
            Assert.AreEqual(3, _calledA);
        }

        [Test]
        public void Should_not_hold_reference() {
            var sub = new Sub();
            _mediator.Subscribe<MsgA>(sub.Call);
            Assert.AreEqual(1, _mediator.RegisteredSubscribers());
            sub = null;
            RunGC();
            _mediator.Publish(new MsgA());
            Assert.AreEqual(0, _mediator.RegisteredSubscribers());
        }

        private static void RunGC() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        private void Call(MsgA msg) {
            _calledA++;
        }
        private void Call(MsgB msg) {
            _calledB++;
        }
    }

    public class MsgA { }
    public class MsgB { }

    public class Sub {
        public void Call(MsgA msg) { }
    }
}
