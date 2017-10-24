using System;
using System.Collections.Generic;

namespace MemberRegistry.controller.commands 
{
    class DeleteBoatCommand : CRUDCommand
    {
        public DeleteBoatCommand(string description, view.IView view, model.MemberLedger ledger) 
        : base(description, view, ledger)
        {}
        
        public override void ExecuteCommand() 
        {
            base._currentlySelectedMember = GetMember();
            this.DeleteMemberBoat();
        }

        private void DeleteMemberBoat()
        {
            base.DisplayBoats();
            model.Boat boat = base.GetBoat();
            _ledger.DeleteBoat(base._currentlySelectedMember, boat);
        }
    }
}