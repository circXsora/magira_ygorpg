using System.Collections;
using System.Collections.Generic;
using MGO.Entity;
using MGO.Entity.Unity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EntityTest
{
    // A Test behaves as an ordinary method
    //[Test]
    //public void NewTestScriptSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator UniversalEntityViewTestPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        var entity = new GameObject();
        IEntityView entityView = entity.AddComponent<EntityView>();

        entityView.ToActive();
        Assert.That(entity.activeSelf);

        entityView.ToDeactive();
        Assert.That(!entity.activeSelf);

        entityView.Position = new Vector3(10, 10, 10);
        yield return null;
        Assert.That(entity.transform.position, Is.EqualTo(new Vector3(10, 10, 10)));

        entityView.LocalPosition = new Vector3(15, 15, 15);
        yield return null;
        Assert.That(entity.transform.localPosition, Is.EqualTo(new Vector3(15, 15, 15)));

        entityView.Rotation = Quaternion.Euler(10, 10, 10);
        yield return null;
        Assert.That(entity.transform.rotation, Is.EqualTo(Quaternion.Euler(10, 10, 10)));

        entityView.LocalRotation = Quaternion.Euler(13, 13, 13);
        yield return null;
        Assert.That(entity.transform.localRotation, Is.EqualTo(Quaternion.Euler(13, 13, 13)));

        entityView.Scale = new Vector3(10, 10, 10);
        yield return null;
        Assert.That(entity.transform.lossyScale, Is.EqualTo(new Vector3(10, 10, 10)));

        entityView.LocalScale = new Vector3(5, 5, 5);
        yield return null;
        Assert.That(entity.transform.localScale, Is.EqualTo(new Vector3(5, 5, 5)));
    }
}
