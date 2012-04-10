using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryExcecutionEngine
{

    public interface IExcecutionEngineService
    {
        /// <summary>
        /// Initializes the instance.
        /// Initialization will only be called once in lifetime.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses all activity in scheudler.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes all acitivity in server.
        /// </summary>
        void Resume();
    }
}
