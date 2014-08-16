using app.bus;
using app.container;
using app.web.application.orders;
using app.web.core;

namespace app.web.application.catalog_browsing
{
  public class SubmitOrder :ISupportAUserStory
  {
    IPublishEvents bus;

    public SubmitOrder(IPublishEvents bus)
    {
      this.bus = bus;
    }

    public SubmitOrder() : this(Dependencies.fetch.an<IPublishEvents>())
    {
      
    }

    public void process(IProvideRequestDetails input)
    {
      bus.publish(input.map<SubmitOrderInput>());
    }
  }
}