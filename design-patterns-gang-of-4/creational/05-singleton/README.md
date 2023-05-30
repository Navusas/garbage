## Singleton

Page: 127

**Summary**

Ensure a class has only one instance, and provide a global point of access to it.

**How do you ensure that there is only 1 instance present?** - Let the instance itself responsible for keeping track of its sole instance.

  
In the modern C#, you would do singleton using. The preference is in this order:
- Dependency injection (like Autofac)
- Or, using Lazy<T>
- Or, using static constructor