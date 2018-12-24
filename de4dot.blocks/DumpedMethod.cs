/*
    Copyright (C) 2011-2015 de4dot@gmail.com

    This file is part of de4dot.

    de4dot is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    de4dot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with de4dot.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace de4dot.blocks {
	[Serializable]
	public class DumpedMethod {
		[NonSerialized]
		private ushort mhFlags;          // method header Flags
		[NonSerialized]
		private ushort mhMaxStack;       // method header MaxStack
		[NonSerialized]
		private uint mhCodeSize;         // method header CodeSize
		[NonSerialized]
		private uint mhLocalVarSigTok;   // method header LocalVarSigTok

		[NonSerialized]
		private uint mdRVA;              // methodDef RVA
		[NonSerialized]
		private ushort mdImplFlags;      // methodDef ImplFlags
		[NonSerialized]
		private ushort mdFlags;          // methodDef Flags
		[NonSerialized]
		private uint mdName;             // methodDef Name (index into #String)
		[NonSerialized]
		private uint mdSignature;        // methodDef Signature (index into #Blob)
		[NonSerialized]
		private uint mdParamList;        // methodDef ParamList (index into Param table)

		[NonSerialized]
		private uint token;              // metadata token

		[NonSerialized]
		public byte[] code;
		[NonSerialized]
		public byte[] extraSections;

		public ushort MhFlags { get => mhFlags; set => mhFlags = value; }
		public ushort MhMaxStack { get => mhMaxStack; set => mhMaxStack = value; }
		public uint MhCodeSize { get => mhCodeSize; set => mhCodeSize = value; }
		public uint MhLocalVarSigTok { get => mhLocalVarSigTok; set => mhLocalVarSigTok = value; }
		public uint MdRVA { get => mdRVA; set => mdRVA = value; }
		public ushort MdImplFlags { get => mdImplFlags; set => mdImplFlags = value; }
		public ushort MdFlags { get => mdFlags; set => mdFlags = value; }
		public uint MdName { get => mdName; set => mdName = value; }
		public uint MdSignature { get => mdSignature; set => mdSignature = value; }
		public uint MdParamList { get => mdParamList; set => mdParamList = value; }
		public uint Token { get => token; set => token = value; }
		public byte[] Code { get => code; set => code = value; }
		public byte[] ExtraSections { get => extraSections; set => extraSections = value; }
	}
}
