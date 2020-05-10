[![KGy SOFT .net](https://docs.kgysoft.net/corelibraries/icons/logo.png)](https://kgysoft.net)

# KGySoft.ComponentModelDemo
This repo is a demo WPF/WinForms application that focuses on some features of the [KGySoft.ComponentModel](https://docs.kgysoft.net/corelibraries/?topic=html/N_KGySoft_ComponentModel.htm) namespace of [KGy SOFT Core Libraries](https://kgysoft.net/corelibraries) and also provides some useful solutions for using the KGy SOFT Core Libraries in WPF and Windows Forms applications.

[![Website](https://img.shields.io/website/https/kgysoft.net/corelibraries.svg)](https://kgysoft.net/corelibraries)
[![Online Help](https://img.shields.io/website/https/docs.kgysoft.net/corelibraries.svg?label=online%20help&up_message=available)](https://docs.kgysoft.net/corelibraries)
[![CoreLibraries Repo](https://img.shields.io/github/repo-size/koszeggy/KGySoft.CoreLibraries.svg?label=CoreLibraries)](https://github.com/koszeggy/KGySoft.CoreLibraries)

![KGySoft.ComponentModelDemo](https://kgysoft.net/images/KGySoft.ComponentModelDemo.jpg)

## Table of Contents
1. [A few highlights](#a-few-highlights)
2. [Download](#download)
3. [Useful WPF Components](#useful-wpf-components)
4. [Useful Windows Forms Components](#useful-windows-forms-components)
5. [License](#license)

## A few highlights:
* The [KGySoft.ComponentModelDemo.Model](https://github.com/koszeggy/KGySoft.ComponentModelDemo/tree/master/KGySoft.ComponentModelDemo/Model) namespace demonstrates how to use the various [business object](https://kgysoft.net/corelibraries#business-objects) base types as model classes.
* It contains also some [example commands](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/Model/Commands.cs). See more about KGy SOFT's technology agnostic commands and command bindings on the [website](https://kgysoft.net/corelibraries#command-binding).
* The [KGySoft.ComponentModelDemo.ViewModel](https://github.com/koszeggy/KGySoft.ComponentModelDemo/tree/master/KGySoft.ComponentModelDemo/ViewModel) namespace demonstrates how to create a technology-agnostic ViewModel. The [MainViewModel](https://github.com/koszeggy/KGySoft.ComponentModelDemo/tree/master/KGySoft.ComponentModelDemo/ViewModel/MainViewModel.cs) class is used by a [WPF Window](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Windows/MainWindow.xaml) and [WinForms Form](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWinForms/Forms/MainForm.cs) as well.

## Download
You can download the sources and the binaries as .zip archives [here](https://github.com/koszeggy/KGySoft.ComponentModelDemo/releases).

## Useful WPF Components
* The [KGyCommandAdapter](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Commands/KGyCommandAdapter.cs) class makes possible to use KGy SOFT commands in WPF as traditional Microsoft commands.
* The [EventToCommand](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Commands/EventToKGyCommandExtension.cs) markup extension makes possible to create bindings to KGy SOFT commands directly from XAML like this (see the [MainWindow.xaml](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Windows/MainWindow.xaml) file for more examples):
```xaml
<Button Content="Click Me" Click="{commands:EventToKGyCommand Command={Binding DoSomethingCommand}}"/>
```
* The [EditToolBar](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Controls/EditToolBar.xaml.cs) control can be bound to any undoable/editable object. It also demonstrates the usage of the [KGyCommandAdapter](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Commands/KGyCommandAdapter.cs) class.
* The [ValidationResult](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Validation/ValidationBindingExtension.cs) and [HasValidationResult](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Validation/HasValidationResultExtension.cs) markup extensions make possible to obtain/check validation results of [IValidatingObject](http://docs.kgysoft.net/corelibraries/?topic=html/T_KGySoft_ComponentModel_IValidatingObject.htm) instances directly from XAML like this (see the [MainWindow.xaml](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Windows/MainWindow.xaml) file for more examples):
```xaml
<DataTrigger Value="True" Binding="{validation:HasValidationResult Warning,
                                    Path=TestList/ValidationResults,
                                    PropertyName=IntProp}">
```
* The [ElementAdorner.Template](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Adorners/ElementAdorner.cs) attached property can be used to define a template for a UIElement that will be displayed in the adorner layer. This makes possible creating templates for Warning and Information validation levels similarly to WPF's Validation.ErrorTemplate property (see the [MainWindow.xaml](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWpf/Windows/MainWindow.xaml) file for more examples):
```xaml
<!-- Warning Template -->
<Setter Property="adorners:ElementAdorner.Template">
    <Setter.Value>
        <ControlTemplate>
            <Border BorderBrush="Orange" BorderThickness="3">
                <adorners:AdornedElementPlaceholder/>
            </Border>
        </ControlTemplate>
    </Setter.Value>
</Setter>
```

## Useful Windows Forms Components
* The [EditMenuStrip](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWinForms/Controls/EditMenuStrip.cs) control can be bound to any undoable/editable object.
* The [ValidationResultToErrorProviderAdapter](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWinForms/Components/ValidationResultToErrorProviderAdapter.cs) component can turn an ErrorProvider component to a WarningProvider or InfoProvider. Just drop it on the Windows Forms Designer, and select the provider instance and the severity. If the `DataSource` property provides [IValidatingObject](http://docs.kgysoft.net/corelibraries/?topic=html/T_KGySoft_ComponentModel_IValidatingObject.htm) instances, then the selected provider will display the validation results of the chosen severity.
* The [ToolTipUpdater](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/KGySoft.ComponentModelDemo/ViewWinForms/Commands/TooltipUpdater.cs) is a [command state updater](https://github.com/koszeggy/KGySoft.CoreLibraries#icommandstateupdater) that can sync `ToolTipText` command state for `Control` sources if any of their parents have a `ToolTip` component.

## License
This repository is under the [KGy SOFT License 1.0](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/LICENSE), which is a permissive GPL-like license. It allows you to copy and redistribute the material in any medium or format for any purpose, even commercially. The only thing is not allowed is to distribute a modified material as yours: though you are free to change and re-use anything, do that by giving appropriate credit. See the [LICENSE](https://github.com/koszeggy/KGySoft.ComponentModelDemo/blob/master/LICENSE) file for details.
