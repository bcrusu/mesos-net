//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: mesos/containerizer/containerizer.proto
// Note: requires additional types generated from: mesos/mesos.proto
namespace mesos.containerizer
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Launch")]
  public partial class Launch : global::ProtoBuf.IExtensible
  {
    public Launch() {}
    
    private mesos.ContainerID _container_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"container_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public mesos.ContainerID container_id
    {
      get { return _container_id; }
      set { _container_id = value; }
    }
    private mesos.TaskInfo _task_info = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"task_info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public mesos.TaskInfo task_info
    {
      get { return _task_info; }
      set { _task_info = value; }
    }
    private mesos.ExecutorInfo _executor_info = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"executor_info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public mesos.ExecutorInfo executor_info
    {
      get { return _executor_info; }
      set { _executor_info = value; }
    }
    private string _directory = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"directory", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string directory
    {
      get { return _directory; }
      set { _directory = value; }
    }
    private string _user = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"user", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string user
    {
      get { return _user; }
      set { _user = value; }
    }
    private mesos.SlaveID _slave_id = null;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"slave_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public mesos.SlaveID slave_id
    {
      get { return _slave_id; }
      set { _slave_id = value; }
    }
    private string _slave_pid = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"slave_pid", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string slave_pid
    {
      get { return _slave_pid; }
      set { _slave_pid = value; }
    }
    private bool _checkpoint = default(bool);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"checkpoint", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool checkpoint
    {
      get { return _checkpoint; }
      set { _checkpoint = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Update")]
  public partial class Update : global::ProtoBuf.IExtensible
  {
    public Update() {}
    
    private mesos.ContainerID _container_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"container_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public mesos.ContainerID container_id
    {
      get { return _container_id; }
      set { _container_id = value; }
    }
    private readonly global::System.Collections.Generic.List<mesos.Resource> _resources = new global::System.Collections.Generic.List<mesos.Resource>();
    [global::ProtoBuf.ProtoMember(2, Name=@"resources", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<mesos.Resource> resources
    {
      get { return _resources; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Wait")]
  public partial class Wait : global::ProtoBuf.IExtensible
  {
    public Wait() {}
    
    private mesos.ContainerID _container_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"container_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public mesos.ContainerID container_id
    {
      get { return _container_id; }
      set { _container_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Destroy")]
  public partial class Destroy : global::ProtoBuf.IExtensible
  {
    public Destroy() {}
    
    private mesos.ContainerID _container_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"container_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public mesos.ContainerID container_id
    {
      get { return _container_id; }
      set { _container_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Usage")]
  public partial class Usage : global::ProtoBuf.IExtensible
  {
    public Usage() {}
    
    private mesos.ContainerID _container_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"container_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public mesos.ContainerID container_id
    {
      get { return _container_id; }
      set { _container_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Termination")]
  public partial class Termination : global::ProtoBuf.IExtensible
  {
    public Termination() {}
    
    private bool _killed;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"killed", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool killed
    {
      get { return _killed; }
      set { _killed = value; }
    }
    private string _message;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string message
    {
      get { return _message; }
      set { _message = value; }
    }
    private int _status = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int status
    {
      get { return _status; }
      set { _status = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Containers")]
  public partial class Containers : global::ProtoBuf.IExtensible
  {
    public Containers() {}
    
    private readonly global::System.Collections.Generic.List<mesos.ContainerID> _containers = new global::System.Collections.Generic.List<mesos.ContainerID>();
    [global::ProtoBuf.ProtoMember(1, Name=@"containers", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<mesos.ContainerID> containers
    {
      get { return _containers; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}