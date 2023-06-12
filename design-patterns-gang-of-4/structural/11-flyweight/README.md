## Flyweight

Page: 195

**Summary**

The Flyweight Pattern uses sharing to support large numbers of fine-grained objects efficiently.


**Applicability**

Use the Flyweight Pattern when and only when all of the below are true:
1. An application uses a large number of objects.
2. Storage costs are high because of the sheer quantity of objects.
3. Most object state can be made extrinsic.
4. Many groups of objects may be replaced by relatively few shared objects once extrinsic state is removed.
5. The application doesn't depend on object identity. Since flyweight objects may be shared, identity tests will return true for conceptually distinct objects.
