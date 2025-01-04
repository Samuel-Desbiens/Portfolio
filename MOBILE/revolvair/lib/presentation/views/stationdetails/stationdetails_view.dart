import 'package:flutter/material.dart';
import 'package:resolvair/presentation/views/alertes/alerte_view.dart';
import 'package:resolvair/presentation/views/commentaires/commentaires_view.dart';
import 'package:resolvair/presentation/views/stationdetails/stationdetails_viewmodel.dart';
import 'package:resolvair/presentation/views/statistique/statistique_view.dart';
import 'package:badges/badges.dart' as badges;
import 'package:stacked/stacked.dart';

class StationDetailsView extends StatelessWidget {
  const StationDetailsView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments;
    return ViewModelBuilder<StationDetailsViewModel>.reactive(
        viewModelBuilder: () => StationDetailsViewModel(),
        onViewModelReady: (viewModel) => viewModel.initialize(args.toString()),
        builder: (context, viewModel, child) {
          return DefaultTabController(
            length: 5,
            child: Scaffold(
              appBar: AppBar(
                backgroundColor: Colors.black,
                title: Text(args.toString()),
                actions: [
                  if (viewModel.token != '')
                    IconButton(
                        onPressed: viewModel.addToFavorite,
                        icon: const Icon(Icons.favorite))
                ],
                bottom: TabBar(
                  tabs: [
                    const Tab(icon: Icon(Icons.show_chart)),
                    const Tab(icon: Icon(Icons.calendar_month)),
                    const Tab(icon: Icon(Icons.calendar_month)),
                    Tab(
                      child: int.parse(viewModel.alertsQte) >= 1
                          ? badges.Badge(
                              position: badges.BadgePosition.custom(
                                  start: 10, top: -10),
                              showBadge: true,
                              badgeContent:
                                  Text(viewModel.alertsQte.toString()),
                              child: const Icon(Icons.warning),
                            )
                          : const Icon(Icons.warning),
                    ),
                    const Tab(icon: Icon(Icons.message)),
                  ],
                ),
              ),
              body: TabBarView(
                children: [
                  const Center(
                    child: StatistiqueView(),
                  ),
                  const Center(
                    child: null,
                  ),
                  const Center(
                    child: null,
                  ),
                  Center(
                    child: AlerteView(),
                  ),
                  Center(
                    child: CommentairesView(),
                  ),
                ],
              ),
            ),
          );
        });
  }
}
