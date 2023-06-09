## Command

Page: 

**Summary**

Encapsulate a request as an object, hence making the same functionality available from different places (like a button and a menu).

**Applicability**

- the OO form of a callback
- specify, queue and execute requests at different times. The lifetime of the Command Object can differ from the original
request. Someties the Command can be passed across to another process.
- we can extend to support Undo
- a single place to log the change
- a Command object offers a way to model transations and macros
