## Facade

Page: 185

**Summary**

The Facade Pattern provides a unified interface to a set of interfaces in a subsystem. Facade defines a higher-level
interface that makes the subsystem easier to use.

**Applicability**
Use the Facade Pattern when you:

- need to have a limited but straightforward interface to a complex subsystem.
- want to layer your subsystems. Use a facade to define an entry point to each subsystem level. If subsystems are
  dependent, then you can simplify the dependencies between them by making them communicate with each other solely
  through their facades.
- want to wrap a poorly designed collection of APIs with a single well-designed API.
- are building a library or framework and you want to provide a simple interface for a complex subsystem.