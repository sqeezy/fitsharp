﻿// Copyright © 2009 Syterra Software Inc.
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License version 2.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

using System;
using System.Collections;
using System.Text;
using fitSharp.Fit.Model;
using fitSharp.Machine.Engine;
using fitSharp.Machine.Model;

namespace fit.Operators {
    public class ComposeDefault : ComposeOperator<Cell> {
        public bool TryCompose(Processor<Cell> processor, TypedValue instance, ref Tree<Cell> result) {
            var newCell = new Parse("td", GetValueString(instance), null, null);
            newCell.SetAttribute(CellAttributes.AddKey, string.Empty);
            result = newCell;
            return true;
        }

        private static string GetValueString(TypedValue instance) {
            string valueString = string.Empty;
            if (!instance.IsNullOrEmpty) {
                if (instance.Value is Array) {
                    var arrayString = new StringBuilder();
                    foreach (object value in (IEnumerable)instance.Value) {
                        if (arrayString.Length > 0) arrayString.Append(", ");
                        arrayString.Append(value.ToString());
                    }
                    valueString =  arrayString.ToString();
                }
                else
                    valueString = instance.Value.ToString();
            }
            return valueString;
        }
    }
}