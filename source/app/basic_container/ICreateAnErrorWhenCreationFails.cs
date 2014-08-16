using System;

namespace app.basic_container
{
  public delegate Exception ICreateAnErrorWhenCreationFails(Exception inner, Type type_that_could_not_be_created);
}