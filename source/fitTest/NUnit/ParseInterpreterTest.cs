﻿// Copyright © 2015 Syterra Software Inc.
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License version 2.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

using System.Reflection;
using fit.Test.Double;
using fitSharp.Fit.Engine;
using fitSharp.Fit.Operators;
using fitSharp.Machine.Model;
using NUnit.Framework;

namespace fit.Test.NUnit {
    [TestFixture] public class ParseInterpreterTest{

        [Test] public void FixtureIsCreated() {
            var result = Parse("<table><tr><td>sample do</td></tr></table>");
            VerifyResult<SampleDoFixture>(result);
        }

        [Test] public void DomainClassIsWrapped() {
            var result = Parse("<table><tr><td>sample domain</td></tr></table>");
            var wrapper = VerifyResult<DefaultFlowInterpreter>(result);
            Assert.IsNotNull(wrapper.SystemUnderTest as SampleDomain);
        }

        [Test] public void FixtureWithSUTIsCreated() {
            var result = Parse("<table><tr><td>sample do</td></tr></table>", new TypedValue("target"));
            var sampleDo = VerifyResult<SampleDoFixture>(result);
            Assert.AreEqual("target", sampleDo.SystemUnderTest.ToString());
        }

        TypedValue Parse(string inputTables) {
            return Parse(inputTables, TypedValue.Void);
        }

        TypedValue Parse(string inputTables, TypedValue target) {
            processor = new Service.Service();
            #if NET5_0
                processor.ApplicationUnderTest.AddAssembly(Assembly.GetExecutingAssembly().Location);
            #else
                processor.ApplicationUnderTest.AddAssembly(Assembly.GetExecutingAssembly().CodeBase);
            #endif
            processor.ApplicationUnderTest.AddNamespace(typeof(SampleDomain).Namespace);
            var parser = new ParseInterpreter { Processor = processor };
            var table = fit.Parse.ParseFrom(inputTables);
            return parser.Parse(typeof(Interpreter), target, table.Parts);
        }

         static T VerifyResult<T>(TypedValue result) where T: class {
            var anObject = result.Value as T;
            Assert.IsNotNull(anObject);
            return anObject;
        }

        CellProcessor processor;
    }
}
