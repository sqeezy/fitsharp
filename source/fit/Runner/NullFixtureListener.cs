// Modified or written by Object Mentor, Inc. for inclusion with FitNesse.
// Copyright (c) 2002 Cunningham & Cunningham, Inc.
// Released under the terms of the GNU General Public License version 2 or later.

using fitSharp.Fit.Model;

namespace fit
{
	public class NullFixtureListener : FixtureListener
	{
		public void TableFinished(Parse finishedTable)
		{
		}
		
		public void TablesFinished(Parse theTables, TestStatus status)
		{
		}
	}
}