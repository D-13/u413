using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Entities;

namespace U413.Domain.Repositories.Objects
{
    public class VariableRepository : IVariableRepository
    {
        private EntityContainer _entityContainer;

        public VariableRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        public void ModifyVariable(Variable variable)
        {
            _entityContainer.SaveChanges();
        }

        public Variable GetVariable(string name)
        {
            return _entityContainer.Variables.SingleOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
