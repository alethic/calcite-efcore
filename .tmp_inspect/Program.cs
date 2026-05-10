using System;
using System.IO;
using System.Linq;
using System.Reflection;
class P { static void Main(string[] args) {
  var asmPath = args[0];
  var typeName = args[1];
  var dir = Path.GetDirectoryName(asmPath);
  var paths = Directory.GetFiles(Path.GetDirectoryName(typeof(object).Assembly.Location), "*.dll").Concat(Directory.GetFiles(dir, "*.dll")).Distinct().ToArray();
  var resolver = new System.Reflection.PathAssemblyResolver(paths);
  using var ctx = new System.Reflection.MetadataLoadContext(resolver);
  var asm = ctx.LoadFromAssemblyPath(asmPath);
  var t = asm.GetTypes().FirstOrDefault(x => x.FullName == typeName || x.Name == typeName);
  if (t == null) { Console.WriteLine("NOT FOUND"); return; }
  Console.WriteLine($"Type: {t.FullName} BaseType: {t.BaseType?.FullName}");
  foreach (var m in t.GetMembers(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static|BindingFlags.DeclaredOnly).OrderBy(m=>m.Name)) Console.WriteLine($"  {m.MemberType}: {m}");
  var bt = t.BaseType;
  while (bt != null) {
    Console.WriteLine($"--- Base: {bt.FullName}");
    foreach (var m in bt.GetMembers(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static|BindingFlags.DeclaredOnly).Where(m=>m.Name.Contains("TestStoreFactory")||m.Name.Contains("AddOptions")||m.Name.Contains("AddNonSharedOptions"))) Console.WriteLine($"  {m.MemberType}: {m}");
    bt = bt.BaseType;
  }
}}
