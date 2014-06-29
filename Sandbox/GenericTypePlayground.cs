using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    [TestClass]
    public class GenericTypePlayground
    {
        class X<T> { }
        class Parent { }
        class Child : Parent { }

        [TestMethod]
        public void GenericTypeIs()
        {
            var xb = new X<bool>();

            Assert.IsTrue(xb is X<bool>);
        }

        [TestMethod]
        public void ChildIsParent()
        {
            var p = new Parent();
            var c = new Child();

            Assert.IsTrue(p is Parent);
            Assert.IsTrue(c is Parent);
            Assert.IsTrue(c is Child);
        }

        [TestMethod]
        public void GenericTypesDiffer()
        {
            var xb = new X<bool>();

            Assert.IsFalse(xb is X<string>);
        }

        [TestMethod]
        public void GenericTypesAreSame()
        {
            var xb1 = new X<bool>();
            var xb2 = new X<bool>();

            Assert.AreEqual(xb1.GetType(), xb2.GetType());
            Assert.AreEqual(typeof(X<bool>), xb1.GetType());
        }

        [TestMethod]
        public void ListCreation()
        {
            var l = new List<string> {
                "Wolfgang",
                "Howey"
            };

            Assert.AreEqual(2, l.Count);
        }

        [TestMethod]
        public void SubUpIntegers()
        {
            var l = new List<int> { 1, 2, 3, 4, 5 };

            int sum = l.Sum();

            Assert.AreEqual(15, sum);
        }

        [TestMethod]
        public void QueueDemo()
        {
            var q = new Queue<string>();

            q.Enqueue("hello");
            q.Enqueue("world");

            Assert.AreEqual(2, q.Count);
            Assert.AreEqual("hello", q.Dequeue());
            Assert.AreEqual("world", q.Dequeue());
            Assert.AreEqual(0, q.Count);
        }

        [TestMethod]
        public void StackDemo()
        {
            var s = new Stack<string>();

            s.Push("phone");
            s.Push("fax");

            Assert.AreEqual(2, s.Count);
            Assert.AreEqual("fax", s.Pop());
            Assert.AreEqual("phone", s.Pop());
            Assert.AreEqual(0, s.Count);
        }
    }
}
