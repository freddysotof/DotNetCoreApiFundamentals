﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFundamentals.Services
{
    public class ClaseB : IClaseB
    {
        private readonly ILogger<ClaseB> logger;

        public ClaseB(ILogger<ClaseB> logger)
        {
            this.logger = logger;
        }
        public void HacerAlgo()
        {
            logger.LogInformation("Ejecutando metodo hacer algo");
        }
    }

    public class ClaseB2 : IClaseB
    {
        public void HacerAlgo()
        {
        }
    }
}