## Chain of Responsibility

Page: 223

**Summary**

Avoid coupling between a sender and a target that can handle the message. 
Instead the sender passes the message to a chain of objects, each of which 
can handle the message or instead pass it to the next in the chain until
one of them handles it.

Note: the message can just drop off the end of the chain


**Applicability**

- more than one object can handle a given request, and handler should be found automatically
- as a client, you don;t want to have to find the required target
- the set of handlers can be dynamically changed

