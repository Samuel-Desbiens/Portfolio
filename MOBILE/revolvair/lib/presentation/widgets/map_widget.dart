import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:latlong2/latlong.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/presentation/views/home/home_viewmodel.dart';
import 'package:stacked/stacked.dart';

class MapWidget extends ViewModelWidget<HomeViewModel> {
  final List<Station> stations;

  const MapWidget({Key? key, required this.stations}) : super(key: key);

  @override
  Widget build(BuildContext context, HomeViewModel viewModel) {
    return Stack(
      children: [
        FlutterMap(
          mapController: viewModel.mapController,
          options: MapOptions(
            center: viewModel.position,
            zoom: 14.0,
            onPositionChanged: (position, hasGesture) {
              if (hasGesture) {
                viewModel.handleMapMove();
              }
            },
          ),
          children: [
            TileLayer(
              urlTemplate: 'https://tile.openstreetmap.org/{z}/{x}/{y}.png',
              userAgentPackageName: 'com.example.app',
            ),
            MarkerLayer(
              markers: _buildMarkers(viewModel),
            ),
            MarkerLayer(
              markers: [_buildUserMarker(viewModel)],
            ),
          ],
        ),
        Positioned(
          bottom: 16.0,
          right: 16.0,
          child: FloatingActionButton(
            onPressed: () async {
              if (viewModel.isLocalise) {
                viewModel.fetchPosition();
              } else {
                viewModel.handleGeolocalisationPermissions();
              }
            },
            child: viewModel.isLocalise && !viewModel.isMapMoved
                ? const Icon(Icons.gps_fixed)
                : viewModel.isMapMoved && viewModel.isLocalise
                    ? const Icon(Icons.location_searching)
                    : const Icon(Icons.location_disabled),
          ),
        ),
      ],
    );
  }

  List<Marker> _buildMarkers(HomeViewModel viewModel) {
    return stations.map((station) {
      return Marker(
        width: 30.0,
        height: 30.0,
        point: LatLng(station.lat, station.long),
        rotate: true,
        builder: (context) => IconButton(
          icon: Icon(Icons.place,
              color: viewModel.get24HStationValuesColor(station.id),
              size: 25.0),
          onPressed: () {
            viewModel.navigateToStationStatsPage(slug: station.slug);
          },
        ),
      );
    }).toList();
  }

  Marker _buildUserMarker(HomeViewModel viewModel) {
    return Marker(
      width: 30.0,
      height: 30.0,
      point: viewModel.position,
      builder: (context) => const Icon(
        Icons.adjust,
        color: Color.fromARGB(255, 219, 104, 10),
        size: 25.0,
      ),
    );
  }
}
