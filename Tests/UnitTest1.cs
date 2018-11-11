using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PredicateCacheFramework;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private PredicateCacheBuilder<Message> _predicateCacheBuilder;

        [TestInitialize()]
        public void Initialize()
        {
           _predicateCacheBuilder = new PredicateCacheBuilder<Message>();

            _predicateCacheBuilder.Add(msg => msg.rofl)
                                 .Add(msg => msg.Content.Equals("Test1"))
                                 .Add(msg => msg.Count == 10);

            _predicateCacheBuilder.Generate();
        }
        
        /// <summary>
        /// test purpose
        /// </summary>
        public class Message
        {
            public bool rofl { get; set; }

            public string Content { get; set; }

            public int Count { get; set; }
        }

        [TestMethod]
        public void Assert_First_Generated_Predicate_Is_True()
        {       
            var result = _predicateCacheBuilder.Get(true, false, false)(new Message { Content = "Test", rofl = true });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Assert_First_Generated_Predicate_Is_False()
        {           
            var result = _predicateCacheBuilder.Get(true, false, false)(new Message { Content = "Test", rofl = false });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Assert_FirstAndSecond_Generated_Predicate_Is_True()
        {      
            var result = _predicateCacheBuilder.Get(true, true, false)(new Message { Content = "Test1", rofl = true });

            Assert.IsTrue(result);
        }



        [TestMethod]
        public void Assert_FirstSecondAndThird_Generated_Predicate_Is_True()
        {
            var result = _predicateCacheBuilder.Get(true, true, true)(new Message { Content = "Test1", rofl = true, Count = 10 });
            Assert.IsTrue(result);
        }



    }
}
