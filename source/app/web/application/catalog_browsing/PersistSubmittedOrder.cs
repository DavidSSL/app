using System;
using app.bus;
using app.container;
using app.web.application.orders;
using app.web.core;

namespace app.web.application.catalog_browsing
{
  public class Bus
  {
    public static void publish<Message>(Message message)
    {
      Dependencies.fetch.an<IPublishEvents>().publish(message);
    }
  }

  public class PersistingFeature : ISupportAUserStory
  {
    ISupportAUserStory feature;
    IStartAUnitOfWork uow;

    public PersistingFeature(ISupportAUserStory feature, IStartAUnitOfWork uow)
    {
      this.feature = feature;
      this.uow = uow;
    }

    public void process(IProvideRequestDetails input)
    {
      using (uow.start())
      {
        feature.process(input);
      }

    }
  }

  public class NestedHandler<T> : IHandle<T>
  {
    IStartAUnitOfWork uow;
    IHandle<T> original;

    public NestedHandler(IHandle<T> original, IStartAUnitOfWork uow)
    {
      this.original = original;
      this.uow = uow;
    }

    public void handle(T message)
    {
      using (uow.start())
      {
        original.handle(message);
      }
    }
  }

  public class UnitOfWork :IStartAUnitOfWork, IQueueObjectsForPersistence
  {
    public IStartAUnitOfWork start()
    {
      return new NonDisposableUOW(this);
    }

    public void Dispose()
    {
    
    }

    public void push<T>(T item_to_persist)
    {
      //queue up
    }
  }

  class NonDisposableUOW : IStartAUnitOfWork, IQueueObjectsForPersistence
  {
    UnitOfWork unit_of_work;

    public NonDisposableUOW(UnitOfWork unit_of_work)
    {
      this.unit_of_work = unit_of_work;
    }

    public IStartAUnitOfWork start()
    {
      return this;
    }

    public void Dispose()
    {
    }

    public void push<T>(T item_to_persist)
    {
      unit_of_work.push(item_to_persist);
    }
  }

  public class PersistSubmittedOrder : IHandle<SubmitOrderInput>
  {
    IPublishEvents bus;
    IQueueObjectsForPersistence unit_of_work;

    public PersistSubmittedOrder(IPublishEvents bus, IQueueObjectsForPersistence unit_of_work)
    {
      this.bus = bus;
      this.unit_of_work = unit_of_work;
    }

    public void handle(SubmitOrderInput message)
    {
      unit_of_work.push(message);
    }
  }

  public interface IQueueObjectsForPersistence
  {
    void push<T>(T item_to_persist);
  }

  public interface IStartAUnitOfWork : IDisposable
  {
    IStartAUnitOfWork start();
  }
}