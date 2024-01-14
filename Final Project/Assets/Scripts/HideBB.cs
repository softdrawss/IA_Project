using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine.AI;

[Action("MyActions/Hide")]
[Help("Get the Vector3 for hiding.")]
public class HideBB : BasePrimitiveAction
{
    [InParam("Navmesh agent")]
    [Help("Cop from which to hide")]
    public NavMeshAgent Agent;

    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject Gameobject;

    [OutParam("hide")]
    [Help("Vector3 for higing.")]
    public Vector3 hide;

    public override TaskStatus OnUpdate()
    {
        Moves moves = Gameobject.GetComponent<Moves>();

        hide = moves.Hide(Agent);

        return TaskStatus.COMPLETED;
    }
}



