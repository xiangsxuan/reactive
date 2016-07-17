﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    
    public partial class AsyncTests
    {
        public void AssertThrows<E>(Action a)
            where E : Exception
        {
            Assert.Throws<E>(a);
        }

        [Obsolete("Don't use this, use Assert.ThrowsAsync and await it", true)]
        public Task AssertThrows<E>(Func<Task> func)
            where E : Exception
        {
            return Assert.ThrowsAsync<E>(func);
        }

        public void AssertThrows<E>(Action a, Func<E, bool> assert)
            where E : Exception
        {

            var hasFailed = false;

            try
            {
                a();
            }
            catch (E e)
            {
                Assert.True(assert(e));
                hasFailed = true;
            }

            if (!hasFailed)
            {
                Assert.True(false);
            }
        }

        public void NoNext<T>(IAsyncEnumerator<T> e)
        {
            Assert.False(e.MoveNext().Result);
        }

        public void HasNext<T>(IAsyncEnumerator<T> e, T value)
        {
            Assert.True(e.MoveNext().Result);
            Assert.Equal(value, e.Current);
        }
    }
}