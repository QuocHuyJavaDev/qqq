using MyNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Service
{
    public interface IMLModel
    {
        Task<string> predict(ModelInput input);
    }
}
