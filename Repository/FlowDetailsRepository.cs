using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class FlowDetailsRepository : GenericRepository<FlowDetails>
    {
        public FlowDetailsRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext) { }

        public List<FlowDetails> GetFlowDetailsListByFlowId(int? flowId)
        {
            return All().Where(x => x.FlowId == flowId).ToList();
        }

        public int? GetFirstPromptId(List<FlowDetails> flowDetails) 
        {
            return flowDetails.FirstOrDefault()?.FlowId;
        }
    }
}
