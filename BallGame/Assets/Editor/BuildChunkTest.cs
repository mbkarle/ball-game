using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Linq;

public class BuildChunkTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
        GameObject gameObject = new GameObject();
		gameObject.AddComponent<BuildWorld> ();

        //Act
        //Try to build dictionary
		gameObject.GetComponent<BuildWorld>().BuildChunkDictionary(1);

        //Assert
        //The dictionary now has 2 keys
		Assert.AreEqual(gameObject.GetComponent<BuildWorld>().chunkDictionary.Keys.ToList().Count, 2);
    }
}
