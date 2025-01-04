import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/home/home_viewmodel.dart';
import 'package:resolvair/presentation/widgets/drawer_widget.dart';
import 'package:resolvair/presentation/widgets/map_widget.dart';
import 'package:stacked/stacked.dart';

class HomeView extends StatefulWidget {
  const HomeView({Key? key}) : super(key: key);

  @override
  State<HomeView> createState() => _HomeViewState();
}

class _HomeViewState extends State<HomeView> with WidgetsBindingObserver {
  late HomeViewModel _homeViewModel;
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    switch (state) {
      case AppLifecycleState.resumed:
        _homeViewModel.loadToken();
        break;
      case AppLifecycleState.paused:
        _homeViewModel.storeToken();
        break;
      case AppLifecycleState.inactive:
        _homeViewModel.storeToken();
        break;
      case AppLifecycleState.detached:
        _homeViewModel.storeToken();
        break;
      case AppLifecycleState.hidden:
        _homeViewModel.storeToken();
        break;
    }
  }

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<HomeViewModel>.reactive(
        viewModelBuilder: () => HomeViewModel(),
        onViewModelReady: (viewModel) => viewModel.initialize(),
        builder: (context, viewModel, child) {
          _homeViewModel = viewModel;
          return MaterialApp(
            home: Scaffold(
              appBar: AppBar(
                backgroundColor: Colors.black,
                title: Text(LocaleKeys.app_revolvair_text.tr()),
                actions: [
                  IconButton(
                    icon: const Icon(
                      Icons.search,
                      color: Colors.white,
                      size: 32,
                    ),
                    onPressed: () {
                      viewModel.navigateToSearchPage();
                    },
                  ),
                ],
              ),
              drawer: const DrawerWidget(),
              body: MapWidget(
                stations: viewModel.stations,
                key: const Key("mapwidget"),
              ),
            ),
          );
        });
  }
}
