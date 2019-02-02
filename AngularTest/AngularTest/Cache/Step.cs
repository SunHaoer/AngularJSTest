using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Cache
{
    public class Step
    {
        public Dictionary<long, Stack<int>> step;
    }
}

/*
 * userId -> statck
 * 0 -> choosePage
 * 1 -> addPhonePage
 * 2 -> addPhoneCheckPage
 * 3 -> deletePage
 * 4 -> deleteCheckPage
 * 5 -> replacePage
 * 6 -> replaceCheckPage
 * 7 -> successPage
 */
