using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using KGySoft.Reflection;

namespace BindingTest.ViewWpf.Adorners
{
  public class AdornerBindingExtension : MarkupExtension
  {
      public string Path { get; set; }

      public AdornerBindingExtension()
      {
      }

      public AdornerBindingExtension(string path)
      {
          Path = path;
      }


      public override object ProvideValue(IServiceProvider serviceProvider)
      {
          var b1 = new Binding { RelativeSource = RelativeSource.TemplatedParent,
              Path = String.IsNullOrWhiteSpace(Path) 
                  ? new PropertyPath(ElementAdorner.AdornedParentProperty)
              : new PropertyPath($"(0).{Path}", ElementAdorner.AdornedParentProperty)
          };
          return b1;
      }
  }


    //[TypeConverter(typeof(AdornerBindingConverter))]
    //[MarkupExtensionReturnType(typeof(object))]
    //public class AdornerBinding : Binding
    //{
    //    private DependencyProperty property;

    //    public AdornerBinding()
    //    {
    //        RelativeSource = RelativeSource.TemplatedParent;
    //        Path = new PropertyPath(ElementAdorner.AdornedParentProperty);
    //    }

    //    public AdornerBinding(DependencyProperty property) : this()
    //    {
    //        Property = property;
    //    }


    //    [ConstructorArgument("property")]
    //    public DependencyProperty Property
    //    {
    //        get => property;
    //        set
    //        {
    //            property = value;
    //            if (value == null)
    //            {
    //                Path = new PropertyPath(ElementAdorner.AdornedParentProperty);
    //                return;
    //            }

    //            Path = new PropertyPath("(0).(1)", ElementAdorner.AdornedParentProperty, value);
    //        }
    //    }
    //}

    //public class AdornerBindingConverter : TypeConverter
    //{
    //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //    {
    //        if (destinationType == typeof(InstanceDescriptor))
    //        {
    //            return true;
    //        }
    //        return base.CanConvertTo(context, destinationType);
    //    }

    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType == typeof(InstanceDescriptor))
    //        {
    //            if (value == null)
    //                throw new ArgumentNullException(nameof(value));

    //            AdornerBinding adornerBinding = value as AdornerBinding;

    //            if (adornerBinding == null)
    //                throw new ArgumentException();

    //            return new InstanceDescriptor(typeof(AdornerBinding).GetConstructor(new Type[] { typeof(DependencyProperty) }),
    //                new object[] { adornerBinding.Property });
    //        }
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }
    //}

    //public class AdornedParentExtension : MarkupExtension
    //{
    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target))
    //            return null;

    //        return new Binding { RelativeSource = RelativeSource.TemplatedParent, Path = new PropertyPath(ElementAdorner.AdornedParentProperty) };

    //        //var b = new Binding { RelativeSource = RelativeSource.TemplatedParent, Path = new PropertyPath(ElementAdorner.AdornedParentProperty)};
    //        //var adornedParent = b.ProvideValue(serviceProvider);
    //        //var dobj = target.TargetObject as FrameworkElement;
    //        //var prop = target.TargetProperty;

    //        //var templatedParent = dobj.TemplatedParent;

    //        //if (!(target.TargetProperty is PropertyInfo targetProperty))
    //        //    return null;

    //        //if (targetProperty == Reflector.MemberOf(() => default(Binding).RelativeSource))
    //        //{
    //        //    return RelativeSource.TemplatedParent;
    //        //    //return new PropertyPath(ElementAdorner.AdornedParentProperty);

    //        //}
    //        throw new NotSupportedException();
    //        //Type helperType = Reflector.ResolveType("MS.Internal.Helper");
    //        //var templatedParent = new Binding { RelativeSource = RelativeSource.TemplatedParent }.ProvideValue(serviceProvider);
    //        //object[] args = { new Binding{ RelativeSource = RelativeSource.TemplatedParent}, serviceProvider, null, null };



    //        //var obj = target.TargetObject;
    //        //var prop = target.TargetProperty;



    //        //var binding = (Binding)obj;
    //        //var source = binding.Source;
    //        //var bindingresult = binding.ProvideValue(serviceProvider);
    //        //return obj;
    //    }

    //    //internal static void CheckCanReceiveMarkupExtension(
    //    //        MarkupExtension markupExtension,
    //    //        IServiceProvider serviceProvider,
    //    //    out DependencyObject targetDependencyObject,
    //    //    out DependencyProperty targetDependencyProperty)
    //    //{
    //    //    targetDependencyObject = null;
    //    //    targetDependencyProperty = null;

    //    //    IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
    //    //    if (provideValueTarget == null)
    //    //    {
    //    //        return;
    //    //    }

    //    //    object targetObject = provideValueTarget.TargetObject;

    //    //    if (targetObject == null)
    //    //    {
    //    //        return;
    //    //    }

    //    //    Type targetType = targetObject.GetType();
    //    //    object targetProperty = provideValueTarget.TargetProperty;

    //    //    if (targetProperty != null)
    //    //    {
    //    //        targetDependencyProperty = targetProperty as DependencyProperty;
    //    //        if (targetDependencyProperty != null)
    //    //        {
    //    //            // This is the DependencyProperty case

    //    //            targetDependencyObject = targetObject as DependencyObject;
    //    //            Debug.Assert(targetDependencyObject != null, "DependencyProperties can only be set on DependencyObjects");
    //    //        }
    //    //        else
    //    //        {
    //    //            MemberInfo targetMember = targetProperty as MemberInfo;
    //    //            if (targetMember != null)
    //    //            {
    //    //                // This is the Clr Property case
    //    //                PropertyInfo propertyInfo = targetMember as PropertyInfo;

    //    //                // Setters, Triggers, DataTriggers & Conditions are the special cases of
    //    //                // Clr properties where DynamicResource & Bindings are allowed. Normally
    //    //                // these cases are handled by the parser calling the appropriate
    //    //                // ReceiveMarkupExtension method.  But a custom MarkupExtension
    //    //                // that delegates ProvideValue will end up here (see Dev11 117372).
    //    //                // So we handle it similarly to how the parser does it.

    //    //                EventHandler<System.Windows.Markup.XamlSetMarkupExtensionEventArgs> setMarkupExtension
    //    //                    = LookupSetMarkupExtensionHandler(targetType);

    //    //                if (setMarkupExtension != null && propertyInfo != null)
    //    //                {
    //    //                    System.Xaml.IXamlSchemaContextProvider scp = serviceProvider.GetService(typeof(System.Xaml.IXamlSchemaContextProvider)) as System.Xaml.IXamlSchemaContextProvider;
    //    //                    if (scp != null)
    //    //                    {
    //    //                        System.Xaml.XamlSchemaContext sc = scp.SchemaContext;
    //    //                        System.Xaml.XamlType xt = sc.GetXamlType(targetType);
    //    //                        if (xt != null)
    //    //                        {
    //    //                            System.Xaml.XamlMember member = xt.GetMember(propertyInfo.Name);
    //    //                            if (member != null)
    //    //                            {
    //    //                                var eventArgs = new System.Windows.Markup.XamlSetMarkupExtensionEventArgs(member, markupExtension, serviceProvider);

    //    //                                // ask the target object whether it accepts MarkupExtension
    //    //                                setMarkupExtension(targetObject, eventArgs);
    //    //                                if (eventArgs.Handled)
    //    //                                    return;     // if so, all is well
    //    //                            }
    //    //                        }
    //    //                    }

    //    //                }


    //    //                // Find the MemberType

    //    //                Debug.Assert(targetMember is PropertyInfo || targetMember is MethodInfo,
    //    //                    "TargetMember is either a Clr property or an attached static settor method");

    //    //                Type memberType;

    //    //                if (propertyInfo != null)
    //    //                {
    //    //                    memberType = propertyInfo.PropertyType;
    //    //                }
    //    //                else
    //    //                {
    //    //                    MethodInfo methodInfo = (MethodInfo)targetMember;
    //    //                    ParameterInfo[] parameterInfos = methodInfo.GetParameters();
    //    //                    Debug.Assert(parameterInfos.Length == 2, "The signature of a static settor must contain two parameters");
    //    //                    memberType = parameterInfos[1].ParameterType;
    //    //                }

    //    //                // Check if the MarkupExtensionType is assignable to the given MemberType
    //    //                // This check is to allow properties such as the following
    //    //                // - DataTrigger.Binding
    //    //                // - Condition.Binding
    //    //                // - HierarchicalDataTemplate.ItemsSource
    //    //                // - GridViewColumn.DisplayMemberBinding

    //    //                if (!typeof(MarkupExtension).IsAssignableFrom(memberType) ||
    //    //                     !memberType.IsAssignableFrom(markupExtension.GetType()))
    //    //                {
    //    //                    throw new XamlParseException(SR.Get(SRID.MarkupExtensionDynamicOrBindingOnClrProp,
    //    //                                                        markupExtension.GetType().Name,
    //    //                                                        targetMember.Name,
    //    //                                                        targetType.Name));
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                // This is the Collection ContentProperty case
    //    //                // Example:
    //    //                // <DockPanel>
    //    //                //   <Button />
    //    //                //   <DynamicResource ResourceKey="foo" />
    //    //                // </DockPanel>

    //    //                // Collection<BindingBase> used in MultiBinding is a special
    //    //                // case of a Collection that can contain a Binding.

    //    //                if (!typeof(BindingBase).IsAssignableFrom(markupExtension.GetType()) ||
    //    //                    !typeof(Collection<BindingBase>).IsAssignableFrom(targetProperty.GetType()))
    //    //                {
    //    //                    throw new XamlParseException(SR.Get(SRID.MarkupExtensionDynamicOrBindingInCollection,
    //    //                                                        markupExtension.GetType().Name,
    //    //                                                        targetProperty.GetType().Name));
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        // This is the explicit Collection Property case
    //    //        // Example:
    //    //        // <DockPanel>
    //    //        // <DockPanel.Children>
    //    //        //   <Button />
    //    //        //   <DynamicResource ResourceKey="foo" />
    //    //        // </DockPanel.Children>
    //    //        // </DockPanel>

    //    //        // Collection<BindingBase> used in MultiBinding is a special
    //    //        // case of a Collection that can contain a Binding.

    //    //        if (!typeof(BindingBase).IsAssignableFrom(markupExtension.GetType()) ||
    //    //            !typeof(Collection<BindingBase>).IsAssignableFrom(targetType))
    //    //        {
    //    //            throw new XamlParseException(SR.Get(SRID.MarkupExtensionDynamicOrBindingInCollection,
    //    //                                                markupExtension.GetType().Name,
    //    //                                                targetType.Name));
    //    //        }
    //    //    }
    //    //}
    //}
}
