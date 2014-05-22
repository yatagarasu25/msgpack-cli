﻿#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010-2013 FUJIWARA, Yusuke
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
#endregion -- License Terms --
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace MsgPack.Serialization
{
	[TestFixture]
	public class SerializerGeneratorTest
	{
		[Test]
		public void TestWithDefault_DllIsGeneratedOnAppBase()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( typeof( GeneratorTestObject ), name );
			var filePath = Path.GetFullPath( "." + Path.DirectorySeparatorChar + name.Name + ".dll" );
			target.GenerateAssemblyFile();
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( GeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomain(
					filePath,
					PackerCompatibilityOptions.Classic,
					new byte[] { ( byte )'A' },
					new byte[] { MessagePackCode.MinimumFixedArray + 1, MessagePackCode.MinimumFixedRaw + 1, ( byte )'A' },
					TestType.GeneratorTestObject
					);
			}
			finally
			{
				File.Delete( filePath );
			}
		}

		[Test]
		public void TestWithDirectory_DllIsGeneratedOnSpecifiedDirectory()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( typeof( GeneratorTestObject ), name );
			var directory = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString() );
			target.GenerateAssemblyFile( directory );
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( GeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomain(
					Path.Combine( directory, "." + Path.DirectorySeparatorChar + name.Name + ".dll" ),
					PackerCompatibilityOptions.Classic,
					new byte[] { ( byte )'A' },
					new byte[] { MessagePackCode.MinimumFixedArray + 1, MessagePackCode.MinimumFixedRaw + 1, ( byte )'A' },
					TestType.GeneratorTestObject
					);
			}
			finally
			{
				Directory.Delete( directory, true );
			}
		}

		[Test]
		public void TestWithWithMethod_OptionsAreAsSpecified()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( typeof( GeneratorTestObject ), name );
			target.Method = SerializationMethod.Map;
			var filePath = Path.GetFullPath( "." + Path.DirectorySeparatorChar + name.Name + ".dll" );
			target.GenerateAssemblyFile();
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( GeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomain(
					filePath,
					PackerCompatibilityOptions.Classic,
					new byte[] { ( byte )'A' },
					new byte[] { MessagePackCode.MinimumFixedMap + 1, MessagePackCode.MinimumFixedRaw + 3, ( byte )'V', ( byte )'a', ( byte )'l',
						MessagePackCode.MinimumFixedRaw + 1, ( byte )'A' },
					TestType.GeneratorTestObject
					);
			}
			finally
			{
				File.Delete( filePath );
			}
		}

		[Test]
		public void TestWithWithPackerOption_OptionsAreAsSpecified()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( typeof( GeneratorTestObject ), name );
			var filePath = Path.GetFullPath( "." + Path.DirectorySeparatorChar + name.Name + ".dll" );
			target.GenerateAssemblyFile();
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( GeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomain(
					filePath,
					PackerCompatibilityOptions.None,
					new byte[] { ( byte )'A' },
					new byte[] { MessagePackCode.MinimumFixedArray + 1, MessagePackCode.Bin8, 1, ( byte )'A' },
					TestType.GeneratorTestObject
					);
			}
			finally
			{
				File.Delete( filePath );
			}
		}

		[Test]
		public void TestComplexType_ChildGeneratorsAreContainedAutomatically()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( typeof( RootGeneratorTestObject ), name );
			var directory = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString() );
			target.GenerateAssemblyFile( directory );
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( RootGeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomain(
					Path.Combine( directory, "." + Path.DirectorySeparatorChar + name.Name + ".dll" ),
					PackerCompatibilityOptions.Classic,
					new byte[] { ( byte )'A' },
					new byte[]
					{
						MessagePackCode.MinimumFixedArray + 2,
						MessagePackCode.MinimumFixedArray + 1, MessagePackCode.MinimumFixedRaw + 1, ( byte ) 'A',
						MessagePackCode.NilValue
					},
					TestType.RootGeneratorTestObject
					);
			}
			finally
			{
				Directory.Delete( directory, true );
			}
		}

		[Test]
		public void TestComplexType_MultipleTypes()
		{
			var name = new AssemblyName( MethodBase.GetCurrentMethod().Name );
			var target = new SerializerGenerator( name );
			target.TargetTypes.Add( typeof( GeneratorTestObject ) );
			target.TargetTypes.Add( typeof( AnotherGeneratorTestObject ) );
			var directory = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString() );
			target.GenerateAssemblyFile( directory );
			// Assert is not polluted.
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( GeneratorTestObject ) ), Is.False );
			Assert.That( SerializationContext.Default.ContainsSerializer( typeof( AnotherGeneratorTestObject ) ), Is.False );

			try
			{
				TestOnWorkerAppDomainForMultiple(
					Path.Combine( directory, "." + Path.DirectorySeparatorChar + name.Name + ".dll" ),
					PackerCompatibilityOptions.Classic,
					new byte[] { ( byte )'A' },
					new byte[]
					{
						MessagePackCode.MinimumFixedArray + 1, MessagePackCode.MinimumFixedRaw + 1, ( byte ) 'A',
					},
					new byte[] { ( byte )'B' },
					new byte[]
					{
						MessagePackCode.MinimumFixedArray + 1, MessagePackCode.MinimumFixedRaw + 1, ( byte ) 'B',
					}
					);
			}
			finally
			{
				Directory.Delete( directory, true );
			}
		}

		private static void TestOnWorkerAppDomain( string geneartedAssemblyFilePath, PackerCompatibilityOptions packerCompatibilityOptions, byte[] bytesValue, byte[] expectedPackedValue, TestType testType )
		{
			var appDomainSetUp = new AppDomainSetup() { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase };
			var workerDomain = AppDomain.CreateDomain( "Worker", null, appDomainSetUp );
			try
			{
				var testerProxy =
					workerDomain.CreateInstanceAndUnwrap( typeof( Tester ).Assembly.FullName, typeof( Tester ).FullName ) as Tester;
				testerProxy.DoTest( geneartedAssemblyFilePath, ( int )packerCompatibilityOptions, bytesValue, expectedPackedValue, 1, testType );
			}
			finally
			{
				AppDomain.Unload( workerDomain );
			}
		}

		private static void TestOnWorkerAppDomainForMultiple( string geneartedAssemblyFilePath, PackerCompatibilityOptions packerCompatibilityOptions, byte[] bytesValue1, byte[] expectedPackedValue1, byte[] bytesValue2, byte[] expectedPackedValue2 )
		{
			var appDomainSetUp = new AppDomainSetup() { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase };
			var workerDomain = AppDomain.CreateDomain( "Worker", null, appDomainSetUp );
			try
			{
				var testerProxy =
					workerDomain.CreateInstanceAndUnwrap( typeof( Tester ).Assembly.FullName, typeof( Tester ).FullName ) as Tester;
				testerProxy.DoTest( geneartedAssemblyFilePath, ( int )packerCompatibilityOptions, bytesValue1, expectedPackedValue1, 2, TestType.GeneratorTestObject );
				testerProxy.DoTest( geneartedAssemblyFilePath, ( int )packerCompatibilityOptions, bytesValue2, expectedPackedValue2, 2, TestType.AnotherGeneratorTestObject );
			}
			finally
			{
				AppDomain.Unload( workerDomain );
			}
		}

		public sealed class Tester : MarshalByRefObject
		{
			public void DoTest( string testAssemblyFile, int packerCompatiblityOptions, byte[] bytesValue, byte[] expectedPackedValue, int expectedSerializerTypeCounts, TestType testType )
			{
				var assembly = Assembly.LoadFrom( testAssemblyFile );
				var types = assembly.GetTypes().Where( t => typeof( IMessagePackSerializer ).IsAssignableFrom( t ) ).ToList();
				Assert.That( types.Count, Is.EqualTo( expectedSerializerTypeCounts ), String.Join( ", ", types.Select( t => t.ToString() ).ToArray() ) );

				var context = new SerializationContext( ( PackerCompatibilityOptions )packerCompatiblityOptions );

				byte[] binary;
				switch ( testType )
				{
					case TestType.GeneratorTestObject:
					{
						var serializer = Activator.CreateInstance( types.Single( t => typeof( MessagePackSerializer<GeneratorTestObject> ).IsAssignableFrom( t ) ), context ) as MessagePackSerializer<GeneratorTestObject>;
						binary = serializer.PackSingleObject( new GeneratorTestObject() { Val = bytesValue } );
						break;
					}
					case TestType.RootGeneratorTestObject:
					{
						var serializer = Activator.CreateInstance( types.Single( t => typeof( MessagePackSerializer<RootGeneratorTestObject> ).IsAssignableFrom( t ) ), context ) as MessagePackSerializer<RootGeneratorTestObject>;
						binary = serializer.PackSingleObject( new RootGeneratorTestObject() { Val = null, Child = new GeneratorTestObject() { Val = bytesValue } } );
						break;
					}
					default:
					{
						var serializer = Activator.CreateInstance( types.Single( t => typeof( MessagePackSerializer<AnotherGeneratorTestObject> ).IsAssignableFrom( t ) ), context ) as MessagePackSerializer<AnotherGeneratorTestObject>;
						binary = serializer.PackSingleObject( new AnotherGeneratorTestObject() { Val = bytesValue } );
						break;
					}
				}
				Assert.That(
					binary,
					Is.EqualTo( expectedPackedValue ),
					"{0} != {1}",
					Binary.ToHexString( binary ),
					Binary.ToHexString( expectedPackedValue ) );
			}
		}
	}

	[Serializable]
	public enum TestType
	{
		GeneratorTestObject,
		RootGeneratorTestObject,
		AnotherGeneratorTestObject
	}

	public sealed class GeneratorTestObject
	{
		public byte[] Val { get; set; }
	}

	public sealed class RootGeneratorTestObject
	{
		public string Val { get; set; }
		public GeneratorTestObject Child { get; set; }
	}

	public sealed class AnotherGeneratorTestObject
	{
		public byte[] Val { get; set; }
	}
}
