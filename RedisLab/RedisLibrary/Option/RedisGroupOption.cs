namespace RedisLab.RedisLibrary.Option
{
    public class RedisGroupOption
    {
        public string? GroupName { get; set; }

        /// <summary>
        ///     default = true
        /// </summary>
        public bool AllowAdmin { get; set; } = true;

        /// <summary>
        ///     default = 3
        /// </summary>
        public int ConnectRetry { get; set; } = 3;

        /// <summary>
        ///     default = 5000
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        ///     default = 3000
        /// </summary>
        public int SyncTimeout { get; set; } = 3000;

        /// <summary>
        ///     default = 60 Seconds
        /// </summary>
        public int ConfigCheckSeconds { get; set; } = 60;

        /// <summary>
        /// default = null
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// default = false
        /// </summary>
        public bool Ssl { get; set; }
        public string? EndPoints { get; set; }

        /// <summary>
        /// If true, Connect will not create a connection while no servers are available (default false by higgs)
        /// </summary>
        public bool AbortConnect { get; set; } = false;

        /// <summary>
        /// default db id is -1, you can pass it to choose differnt id
        /// </summary>
        public int DefaultDatabase { get; set; } = -1;
    }
}
