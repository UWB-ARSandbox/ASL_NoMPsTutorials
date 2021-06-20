# Physics considerations


The peer-to-peer model utilized by ASL shines in collaborative environments
where users can easily claim objects and modify them in a shared setting.
Unfortunately, this decentralized peer-to-peer model falls flat in physics
calculations, as minute differences in timing can have considerable effect
and desynchronization is an issue. This document is intended to discuss some
of the solutions that ASL users have come up with to solve physics problems.

## The problem:

Because there is no "Host" in ASL, any client is capable of updating an object's
position across all other clients. In the case of an object that moves constantly
such as a ball in a sports game, it's not clear who should be in charge of
keeping the position updated.

Desynchronization is most common in situations where all clients can update the
ball's position. In the worst-case scenario where a client simply sets the ball's
shared position wherever it believes it should go, the ball will flicker back and
forth between different positions.

Collisions have also proved to be difficult in ASL. Because the default collider
in Unity uses the gameobject.transform for collision calculations, it's easy
for a collision to happen differently across clients. This will almost certainly
result in a desynchronization if both clients are able to set the shared position
of the object after the collision.

## Solution #1: Make a "host"

While it goes against the design principles of a peer-to-peer network, sometimes
a client/host architecture works best for physics objects. A user can be singled
out as a host in ASL by using:

GameLiftManager.GetInstance().AmLowestPeer().

This user can be put in charge of updating all shared objects. This solution
ensures that there is a shared source of "truth" across the clients, and that
everyone will continue to see the same game world. This solution is recommended
for applications where an object is to be shared equally across all users, or
where users are competing over an object.

The downside of this solution is that users are at the mercy of the host's
network connection for keeping the object updated. It's likely that the host
will have a significantly smoother view of the shared object compared to other
clients. Keep this in mind in competitive applications, as it's likely that the
host will have a considerable advantage if they are a competitor.

## Solution #2: Give control to whoever needs it

In this solution, whichever user needs to use an object takes control of its
physics. Physics will be updated by the user in control until they no longer
need the object or it enters a state where physics do not need to be calculated.
This solution is recommended for collaborative environments where users have
more complex interactions with an object. In these cases, the experience of the
client manipulating the object should be prioritized, and they should not
experience the delays or jittery movement that they may encounter on a client-host
system.

This solution does not lend itself well to situations where one user might want
to interrupt another client's usage of the object, such as a competitive game.
It may also be difficult to decide who is in control of an object if its physics
need to be updated for a long period of time.


# Additional considerations

This section lists some additional tools that may be useful when designing a
system that involves physics in ASL.


## Consideration 1: Updating parameters in a coroutine

Not all shared attributes need to be updated at the fastest rate possible. For
example, it may be sufficient to update a shared object representing a player's
position every second in a collaborative editor. This is advantageous because
it reduces the amount of networked updates, potentially speeding up the update
rate for critical objects.

Variable update rates can be achieved by utilizing a Unity coroutine. An example
of coroutine usage is included below:

    private static GameObject _playerObject = null;
    private static ASLObject _playerAslObject = null;

    private static readonly float UPDATES_PER_SECOND = 2.0f;

    void Start()
    {
        //It's assumed that the ASLObject is initialized here

        StartCoroutine(NetworkedUpdate());
    }


    IEnumerator NetworkedUpdate()
    {
        while (true)
        {
            if(_playerObject == null) //Ensure that object is initialized
                yield return new WaitForSeconds(0.1f);

            _playerAslObject.SendAndSetClaim(() =>
            {
                _playerAslObject.SendAndSetWorldPosition(transform.position);
            });

            yield return new WaitForSeconds(1 / UPDATES_PER_SECOND);
        }
    }

Objects that are subject to collision should be updated at the fastest rate
possible, so placing them into Update() is advised.

## Consideration 2: Client-side updates for smoothness

In some cases, modifying the client-side Unity transform can reduce the
jittery movement of objects shared across clients. The velocity of an object
can be tracked, and additional position updates can be performed on the
transform.position based on anticipated movement. Client-side smoothing is
essential for Unity cameras, which are extremely unpleasant to view if they
move in a jittery fashion.

Objects that players compete over should not be smoothed on the client side as
this may result in the object being difficult to claim or acting unexpectedly
when the true shared position deviates from the client-side position.
