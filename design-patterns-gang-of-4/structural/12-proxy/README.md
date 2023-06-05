## Adapter

Page: 139

**Summary**

Adapter Design Pattern is used to convert one interface to another so that it can be used by another class. This pattern lets classes work together that couldn't otherwise because of incompatible interfaces.

Often the Adapter is responsible for functionality the Adaptee doesn't provide.

Think about it this way:
1. You found an amazing library, which under the hood does exactly waht you want, but it's interface is not what you need.
2. You can't change the library, because it's not yours.
3. So instead, you create your own interface, which matches what your client needs, and wrap the library in your code, which implements your interface, and uses the library under the hood.
4. The interface you created would be called Adapter, and the library would be called Adaptee.


**Applicability**

Use the Adapter pattern when:
1. You want to use an existing class, and its interface does not match the one you need.
2. You want to create a reusable class that cooperates with unrelated or unforeseen classes, that is, classes that don't necessarily have compatible interfaces.
3. You need to use several existing subclasses, but it's impractical to adapt their interface by subclassing every one. An object adapter can adapt the interface of its parent class.


