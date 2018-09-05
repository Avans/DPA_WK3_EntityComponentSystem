# Demo Entity Component System

## How does it work
1. We have a bunch of houses (purple) and some taxis (yellow).
2. People want to get from house to house, taxis will search for them and move them.
3. Every so often new persons will appear and need to get transported.

## Reviewing the code
### Entities
1. Entities: We only have an entity manager. Entities are nothing more than integers.
2. The manager has a list of ints (these are the entities)
3. Furthermore, the manager has for every component a list, based on index (hence the integers for entities
4. The manager has methods for adding and removing entities and adding/removing components.
### Components
1. There is a component class, it's empty, only used for Generics
2. Every other component has some properties, that's it.
3. Every component can be added to an entity (only once, with our manager)
### Systems
1. These are the workers. They ask for entities with components.
2. Lets look at the Move system. It asks for TargetComponent to know which objects have to go somewhere.
3. It asks for the move component with the targetComponent to ask for its velocity.
4. It updates the current location on the positioncomponent.
5. **Remark:** 
    It is a lot of object searching, but based on EntityID it is done quickly because it is stored in an array and gotten by index.
    <br />And: We only retrieve the data we need instead of all the data.
6. **(Optional:)** Let's look at the drawsystem and see it's independent.
7. **Remark 2:** Every component has a position in this case, we could add it to the base component.

## Creating the components for the entities
1. We want taxi's to use fuel, so we create a fuel component:
``` csharp
public class FuelComponent : Component
{
	public int CurrentFuel;
	public int MaxFuel;
}
```
2. Now our factory has to change the taxi making, making use of the fuel component:
```csharp
// EntityFactory (CreateTaxi)
new FuelComponent {  CurrentFuel = 50, MaxFuel = 50 }
```
3. When we have fuel, we need to be able to tank fuel. Create a new component for it.
``` csharp
public class LoadingDocComponent : Component
{
}
```
4. Our factory must be able to create the loading docs (this is optional ofcourse, but it will keep our Program.cs clean)
``` csharp
// EntityFactory
public int CreateLoadingDoc(int x, int y)
{
    return _entityManager.CreateEntity(
        new PositionComponent { X = x, Y = y },
        new LoadingDocComponent(),
        new VisualComponent { Color = ConsoleColor.Green }
    );
}
```
5. Now we can add two loading docs for example to our world:
``` csharp
// Program.cs (CreateEntities)
factory.CreateLoadingDoc(40, 40);
factory.CreateLoadingDoc(10, 10);
```
## Components are ready, now use them in our systems:
All we need to do now is create a new system. This system interacts with the fuel components.

1. We create a new TankFuelSystem.
This system uses code from the SearchForPassengerSystem to get the shortest route. This should be refactored to a new location.
``` csharp
public class TankFuelSystem : BaseSystem
{
    public TankFuelSystem(EntityManager entityManager) : base(entityManager)
    {
    }

    public override void Update(double dt)
    {
        foreach (var entity in EntityManager.GetAllEntities<FuelComponent>())
        {
            // For debugging
            Console.WriteLine(entity.Component.CurrentFuel);

            // Lower fuel
            entity.Component.CurrentFuel--;

            // If fuel too low, go to LoadingDocComponent.
            if (entity.Component.CurrentFuel < entity.Component.MaxFuel * 0.15)
            {
                var currentLocation = EntityManager.GetComponent<PositionComponent>(entity.Id);
                // Closest, taken from search, needs refactoring.
                var closest =
                    (from loadingDoc in EntityManager.GetAllEntities<LoadingDocComponent>()
                        select new
                        {
                            Location = EntityManager.GetComponent<PositionComponent>(loadingDoc.Id)
                        })
                    .Select(doc => new
                    {
                        Location = doc.Location,
                        Distance = Math.Abs(doc.Location.X - currentLocation.X) + Math.Abs(doc.Location.Y - currentLocation.Y)
                    })
                    .OrderBy(x => x.Distance)
                    .FirstOrDefault();

                if (closest != null)
                {
                    // If we are on target, remove it, so component won't search for fuel for now.
                    if (closest.Distance == 0)
                    {
                        entity.Component.CurrentFuel = entity.Component.MaxFuel;
                        EntityManager.RemoveComponentFromEntity<TargetComponent>(entity.Id);
                    }
                    else
                    {
                        EntityManager.AddComponentToEntity(entity.Id, new TargetComponent { X = closest.Location.X, Y = closest.Location.Y });
                    }
                }
            }
        }
    }
}
```
2. Don't forget to use this system, register it to your list of systems:
```csharp
// Program.cs (CreateSystems)
new TankFuelSystem(manager),
```
### Remarks:
Although this code is buggy, we can see that systems interact. Components are extensible and we code data driven.
Systems can all be run on different threads and when extending the program each systems stays in place, minimal code change.