// Generated by ProtoGen, Version=2.4.1.555, Culture=neutral, PublicKeyToken=55f7125234beb589.  DO NOT EDIT!
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace Neco.Proto {
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class Neco {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    internal static pbd::MessageDescriptor internal__static_Neco_Proto_Session__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::Neco.Proto.Session, global::Neco.Proto.Session.Builder> internal__static_Neco_Proto_Session__FieldAccessorTable;
    internal static pbd::MessageDescriptor internal__static_Neco_Proto_Message__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::Neco.Proto.Message, global::Neco.Proto.Message.Builder> internal__static_Neco_Proto_Message__FieldAccessorTable;
    #endregion
    #region Descriptor
    public static pbd::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbd::FileDescriptor descriptor;
    
    static Neco() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpOZWNvLnByb3RvEgpOZWNvLlByb3RvIkkKB1Nlc3Npb24SEQoJcHVibGlj", 
            "S2V5GAEgAigMEhEKCXNpZ25hdHVyZRgCIAIoDBILCgNsYXQYAyACKAESCwoD", 
            "bG9uGAQgAigBIioKB01lc3NhZ2USEQoJc2lnbmF0dXJlGAEgAigMEgwKBGRh", 
          "dGEYAiACKAw="));
      pbd::FileDescriptor.InternalDescriptorAssigner assigner = delegate(pbd::FileDescriptor root) {
        descriptor = root;
        internal__static_Neco_Proto_Session__Descriptor = Descriptor.MessageTypes[0];
        internal__static_Neco_Proto_Session__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::Neco.Proto.Session, global::Neco.Proto.Session.Builder>(internal__static_Neco_Proto_Session__Descriptor,
                new string[] { "PublicKey", "Signature", "Lat", "Lon", });
        internal__static_Neco_Proto_Message__Descriptor = Descriptor.MessageTypes[1];
        internal__static_Neco_Proto_Message__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::Neco.Proto.Message, global::Neco.Proto.Message.Builder>(internal__static_Neco_Proto_Message__Descriptor,
                new string[] { "Signature", "Data", });
        return null;
      };
      pbd::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbd::FileDescriptor[] {
          }, assigner);
    }
    #endregion
    
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Session : pb::GeneratedMessage<Session, Session.Builder> {
    private Session() { }
    private static readonly Session defaultInstance = new Session().MakeReadOnly();
    private static readonly string[] _sessionFieldNames = new string[] { "lat", "lon", "publicKey", "signature" };
    private static readonly uint[] _sessionFieldTags = new uint[] { 25, 33, 10, 18 };
    public static Session DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override Session DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override Session ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::Neco.Proto.Neco.internal__static_Neco_Proto_Session__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<Session, Session.Builder> InternalFieldAccessors {
      get { return global::Neco.Proto.Neco.internal__static_Neco_Proto_Session__FieldAccessorTable; }
    }
    
    public const int PublicKeyFieldNumber = 1;
    private bool hasPublicKey;
    private pb::ByteString publicKey_ = pb::ByteString.Empty;
    public bool HasPublicKey {
      get { return hasPublicKey; }
    }
    public pb::ByteString PublicKey {
      get { return publicKey_; }
    }
    
    public const int SignatureFieldNumber = 2;
    private bool hasSignature;
    private pb::ByteString signature_ = pb::ByteString.Empty;
    public bool HasSignature {
      get { return hasSignature; }
    }
    public pb::ByteString Signature {
      get { return signature_; }
    }
    
    public const int LatFieldNumber = 3;
    private bool hasLat;
    private double lat_;
    public bool HasLat {
      get { return hasLat; }
    }
    public double Lat {
      get { return lat_; }
    }
    
    public const int LonFieldNumber = 4;
    private bool hasLon;
    private double lon_;
    public bool HasLon {
      get { return hasLon; }
    }
    public double Lon {
      get { return lon_; }
    }
    
    public override bool IsInitialized {
      get {
        if (!hasPublicKey) return false;
        if (!hasSignature) return false;
        if (!hasLat) return false;
        if (!hasLon) return false;
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      CalcSerializedSize();
      string[] field_names = _sessionFieldNames;
      if (hasPublicKey) {
        output.WriteBytes(1, field_names[2], PublicKey);
      }
      if (hasSignature) {
        output.WriteBytes(2, field_names[3], Signature);
      }
      if (hasLat) {
        output.WriteDouble(3, field_names[0], Lat);
      }
      if (hasLon) {
        output.WriteDouble(4, field_names[1], Lon);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        return CalcSerializedSize();
      }
    }
    
    private int CalcSerializedSize() {
      int size = memoizedSerializedSize;
      if (size != -1) return size;
      
      size = 0;
      if (hasPublicKey) {
        size += pb::CodedOutputStream.ComputeBytesSize(1, PublicKey);
      }
      if (hasSignature) {
        size += pb::CodedOutputStream.ComputeBytesSize(2, Signature);
      }
      if (hasLat) {
        size += pb::CodedOutputStream.ComputeDoubleSize(3, Lat);
      }
      if (hasLon) {
        size += pb::CodedOutputStream.ComputeDoubleSize(4, Lon);
      }
      size += UnknownFields.SerializedSize;
      memoizedSerializedSize = size;
      return size;
    }
    public static Session ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Session ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Session ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Session ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Session ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Session ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static Session ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static Session ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static Session ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Session ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private Session MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(Session prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<Session, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(Session cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private Session result;
      
      private Session PrepareBuilder() {
        if (resultIsReadOnly) {
          Session original = result;
          result = new Session();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override Session MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::Neco.Proto.Session.Descriptor; }
      }
      
      public override Session DefaultInstanceForType {
        get { return global::Neco.Proto.Session.DefaultInstance; }
      }
      
      public override Session BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is Session) {
          return MergeFrom((Session) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(Session other) {
        if (other == global::Neco.Proto.Session.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasPublicKey) {
          PublicKey = other.PublicKey;
        }
        if (other.HasSignature) {
          Signature = other.Signature;
        }
        if (other.HasLat) {
          Lat = other.Lat;
        }
        if (other.HasLon) {
          Lon = other.Lon;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_sessionFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _sessionFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              result.hasPublicKey = input.ReadBytes(ref result.publicKey_);
              break;
            }
            case 18: {
              result.hasSignature = input.ReadBytes(ref result.signature_);
              break;
            }
            case 25: {
              result.hasLat = input.ReadDouble(ref result.lat_);
              break;
            }
            case 33: {
              result.hasLon = input.ReadDouble(ref result.lon_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasPublicKey {
        get { return result.hasPublicKey; }
      }
      public pb::ByteString PublicKey {
        get { return result.PublicKey; }
        set { SetPublicKey(value); }
      }
      public Builder SetPublicKey(pb::ByteString value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasPublicKey = true;
        result.publicKey_ = value;
        return this;
      }
      public Builder ClearPublicKey() {
        PrepareBuilder();
        result.hasPublicKey = false;
        result.publicKey_ = pb::ByteString.Empty;
        return this;
      }
      
      public bool HasSignature {
        get { return result.hasSignature; }
      }
      public pb::ByteString Signature {
        get { return result.Signature; }
        set { SetSignature(value); }
      }
      public Builder SetSignature(pb::ByteString value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasSignature = true;
        result.signature_ = value;
        return this;
      }
      public Builder ClearSignature() {
        PrepareBuilder();
        result.hasSignature = false;
        result.signature_ = pb::ByteString.Empty;
        return this;
      }
      
      public bool HasLat {
        get { return result.hasLat; }
      }
      public double Lat {
        get { return result.Lat; }
        set { SetLat(value); }
      }
      public Builder SetLat(double value) {
        PrepareBuilder();
        result.hasLat = true;
        result.lat_ = value;
        return this;
      }
      public Builder ClearLat() {
        PrepareBuilder();
        result.hasLat = false;
        result.lat_ = 0D;
        return this;
      }
      
      public bool HasLon {
        get { return result.hasLon; }
      }
      public double Lon {
        get { return result.Lon; }
        set { SetLon(value); }
      }
      public Builder SetLon(double value) {
        PrepareBuilder();
        result.hasLon = true;
        result.lon_ = value;
        return this;
      }
      public Builder ClearLon() {
        PrepareBuilder();
        result.hasLon = false;
        result.lon_ = 0D;
        return this;
      }
    }
    static Session() {
      object.ReferenceEquals(global::Neco.Proto.Neco.Descriptor, null);
    }
  }
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Message : pb::GeneratedMessage<Message, Message.Builder> {
    private Message() { }
    private static readonly Message defaultInstance = new Message().MakeReadOnly();
    private static readonly string[] _messageFieldNames = new string[] { "data", "signature" };
    private static readonly uint[] _messageFieldTags = new uint[] { 18, 10 };
    public static Message DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override Message DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override Message ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::Neco.Proto.Neco.internal__static_Neco_Proto_Message__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<Message, Message.Builder> InternalFieldAccessors {
      get { return global::Neco.Proto.Neco.internal__static_Neco_Proto_Message__FieldAccessorTable; }
    }
    
    public const int SignatureFieldNumber = 1;
    private bool hasSignature;
    private pb::ByteString signature_ = pb::ByteString.Empty;
    public bool HasSignature {
      get { return hasSignature; }
    }
    public pb::ByteString Signature {
      get { return signature_; }
    }
    
    public const int DataFieldNumber = 2;
    private bool hasData;
    private pb::ByteString data_ = pb::ByteString.Empty;
    public bool HasData {
      get { return hasData; }
    }
    public pb::ByteString Data {
      get { return data_; }
    }
    
    public override bool IsInitialized {
      get {
        if (!hasSignature) return false;
        if (!hasData) return false;
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      CalcSerializedSize();
      string[] field_names = _messageFieldNames;
      if (hasSignature) {
        output.WriteBytes(1, field_names[1], Signature);
      }
      if (hasData) {
        output.WriteBytes(2, field_names[0], Data);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        return CalcSerializedSize();
      }
    }
    
    private int CalcSerializedSize() {
      int size = memoizedSerializedSize;
      if (size != -1) return size;
      
      size = 0;
      if (hasSignature) {
        size += pb::CodedOutputStream.ComputeBytesSize(1, Signature);
      }
      if (hasData) {
        size += pb::CodedOutputStream.ComputeBytesSize(2, Data);
      }
      size += UnknownFields.SerializedSize;
      memoizedSerializedSize = size;
      return size;
    }
    public static Message ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Message ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Message ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Message ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Message ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Message ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static Message ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static Message ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static Message ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Message ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private Message MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(Message prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<Message, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(Message cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private Message result;
      
      private Message PrepareBuilder() {
        if (resultIsReadOnly) {
          Message original = result;
          result = new Message();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override Message MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::Neco.Proto.Message.Descriptor; }
      }
      
      public override Message DefaultInstanceForType {
        get { return global::Neco.Proto.Message.DefaultInstance; }
      }
      
      public override Message BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is Message) {
          return MergeFrom((Message) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(Message other) {
        if (other == global::Neco.Proto.Message.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasSignature) {
          Signature = other.Signature;
        }
        if (other.HasData) {
          Data = other.Data;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_messageFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _messageFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              result.hasSignature = input.ReadBytes(ref result.signature_);
              break;
            }
            case 18: {
              result.hasData = input.ReadBytes(ref result.data_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasSignature {
        get { return result.hasSignature; }
      }
      public pb::ByteString Signature {
        get { return result.Signature; }
        set { SetSignature(value); }
      }
      public Builder SetSignature(pb::ByteString value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasSignature = true;
        result.signature_ = value;
        return this;
      }
      public Builder ClearSignature() {
        PrepareBuilder();
        result.hasSignature = false;
        result.signature_ = pb::ByteString.Empty;
        return this;
      }
      
      public bool HasData {
        get { return result.hasData; }
      }
      public pb::ByteString Data {
        get { return result.Data; }
        set { SetData(value); }
      }
      public Builder SetData(pb::ByteString value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasData = true;
        result.data_ = value;
        return this;
      }
      public Builder ClearData() {
        PrepareBuilder();
        result.hasData = false;
        result.data_ = pb::ByteString.Empty;
        return this;
      }
    }
    static Message() {
      object.ReferenceEquals(global::Neco.Proto.Neco.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code