// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyEventInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyEventInfo : EventInfo
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly int _eventToken;
    private int _declaringClassToken;
    private int _eventHandlerTypeToken;
    private readonly GenericContext _context;
    private string _name;
    private readonly int _nameLength;
    private Token _addMethodToken;
    private Token _removeMethodToken;
    private Token _raiseMethodToken;

    public MetadataOnlyEventInfo(
      MetadataOnlyModule resolver,
      Token eventToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      this._resolver = resolver;
      this._eventToken = Token.op_Implicit(eventToken);
      this._context = new GenericContext(typeArgs, methodArgs);
      int pdwEventFlags;
      int pmdAddOn;
      int pmdRemoveOn;
      int pmdFire;
      this._resolver.RawImport.GetEventProps(this._eventToken, out this._declaringClassToken, (StringBuilder) null, 0, out this._nameLength, out pdwEventFlags, out this._eventHandlerTypeToken, out pmdAddOn, out pmdRemoveOn, out pmdFire, out int _, 1U, out uint _);
      this.Attributes = (EventAttributes) pdwEventFlags;
      this._addMethodToken = new Token(pmdAddOn);
      this._removeMethodToken = new Token(pmdRemoveOn);
      this._raiseMethodToken = new Token(pmdFire);
    }

    public virtual string ToString() => ((MemberInfo) this).DeclaringType.ToString() + "." + ((MemberInfo) this).Name;

    private void InitializeName()
    {
      if (!string.IsNullOrEmpty(this._name))
        return;
      IMetadataImport rawImport = this._resolver.RawImport;
      StringBuilder builder = StringBuilderPool.Get(this._nameLength);
      rawImport.GetEventProps(this._eventToken, out this._declaringClassToken, builder, builder.Capacity, out int _, out int _, out this._eventHandlerTypeToken, out int _, out int _, out int _, out int _, 1U, out uint _);
      this._name = builder.ToString();
      StringBuilderPool.Release(ref builder);
    }

    public virtual EventAttributes Attributes { get; }

    public virtual MemberTypes MemberType => MemberTypes.Event;

    public virtual string Name
    {
      get
      {
        this.InitializeName();
        return this._name;
      }
    }

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual Type EventHandlerType => this._resolver.GetGenericType(new Token(this._eventHandlerTypeToken), this._context);

    public virtual Type DeclaringType => this._resolver.GetGenericType(new Token(this._declaringClassToken), this._context);

    public virtual int MetadataToken => this._eventToken;

    public virtual MethodInfo GetAddMethod(bool nonPublic)
    {
      if (((Token) ref this._addMethodToken).IsNil)
        return (MethodInfo) null;
      MethodInfo genericMethodInfo = this._resolver.GetGenericMethodInfo(this._addMethodToken, this._context);
      return nonPublic || ((MethodBase) genericMethodInfo).IsPublic ? genericMethodInfo : (MethodInfo) null;
    }

    public virtual MethodInfo GetRemoveMethod(bool nonPublic)
    {
      if (((Token) ref this._removeMethodToken).IsNil)
        return (MethodInfo) null;
      MethodInfo genericMethodInfo = this._resolver.GetGenericMethodInfo(this._removeMethodToken, this._context);
      return nonPublic || ((MethodBase) genericMethodInfo).IsPublic ? genericMethodInfo : (MethodInfo) null;
    }

    public virtual MethodInfo GetRaiseMethod(bool nonPublic)
    {
      if (((Token) ref this._raiseMethodToken).IsNil)
        return (MethodInfo) null;
      MethodInfo genericMethodInfo = this._resolver.GetGenericMethodInfo(this._raiseMethodToken, this._context);
      return nonPublic || ((MethodBase) genericMethodInfo).IsPublic ? genericMethodInfo : (MethodInfo) null;
    }

    public virtual Module Module => (Module) this._resolver;

    public virtual bool Equals(object obj) => obj is MetadataOnlyEventInfo metadataOnlyEventInfo && ((object) metadataOnlyEventInfo._resolver).Equals((object) this._resolver) && metadataOnlyEventInfo._eventToken.Equals(this._eventToken) && ((MemberInfo) this).DeclaringType.Equals(((MemberInfo) metadataOnlyEventInfo).DeclaringType);

    public virtual int GetHashCode() => ((object) this._resolver).GetHashCode() * (int) short.MaxValue + this._eventToken.GetHashCode();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this._resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);
  }
}
