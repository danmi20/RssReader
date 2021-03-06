﻿using System;
using System.Threading;

namespace PL.Common
{
    /// <summary>
    /// Encapsulates the thread-cancellation mechanism. 
    /// </summary>
    public class Canceller : IDisposable
    {
        /// <summary>
        /// Gets or sets the current token source, which will 
        /// be reset on the next call to the Cancel method. 
        /// </summary>
        private CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        /// <summary>
        /// Gets the token from the current token source, which 
        /// you can cancel with the next call to the Cancel method. 
        /// </summary>
        public CancellationToken Token => TokenSource.Token;

        /// <summary>
        /// Cancels the current Token and resets the TokenSource.
        /// </summary>
        public void Cancel()
        {
            TokenSource.Cancel();
            var t = Token;
            TokenSource.Dispose();
            TokenSource = new CancellationTokenSource();
        }

        /// <summary>Releases resources used by the class.</summary>
        ~Canceller() { Dispose(false); }

        /// <summary>Releases resources used by this class.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases resources used by this class.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; 
        /// false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed) return;
            if (disposing) TokenSource.Dispose();
            _isDisposed = true;
        }
        private bool _isDisposed = false;
    }
}
