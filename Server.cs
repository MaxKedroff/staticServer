using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace staticServer
{
    public static class Server
    {
        private static int _count = 0;
        private static readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();


        public static int GetCount()
        {
            _rwLock.EnterReadLock();
            try
            {
                return _count;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }


        public static void AddToCount(int value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                _count += value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        } 
    }
}
