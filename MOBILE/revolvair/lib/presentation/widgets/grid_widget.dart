import 'package:flutter/material.dart';
import 'package:resolvair/presentation/views/statistique/statistique_viewmodel.dart';
import 'package:syncfusion_flutter_gauges/gauges.dart';

class GridWidget extends StatelessWidget {
  final  StatistiqueViewModel viewModel;

  const GridWidget({Key? key,  required this.viewModel}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GridView.builder(
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2, // Number of columns
      ),
      itemCount: 8, 
      itemBuilder: (BuildContext context, int index) {
        if(index >= 4 && viewModel.statsList[index] != "N/A"){
          return GridTile(
          child: Padding(padding: const EdgeInsets.all(8.0),
          child:
            Container(
              decoration: BoxDecoration(borderRadius: const BorderRadius.all(Radius.circular(8.0)),
                color: const Color.fromARGB(0, 14, 100, 129),
                border: Border.all()
              ),
              child:Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Expanded(child: 
                      SfRadialGauge(
                      axes: <RadialAxis>[
                        RadialAxis(
                          minimum: 0,
                          maximum: 250,
                          interval: 50,
                          ranges: <GaugeRange>[
                            GaugeRange(startValue: 0, endValue: 12,color: const Color.fromARGB(255, 0, 255, 0)),
                            GaugeRange(startValue: 12, endValue: 35,color: const Color.fromARGB(255, 255, 255, 0)),
                            GaugeRange(startValue: 35, endValue: 55,color: const Color.fromARGB(255, 255, 183, 0)),
                            GaugeRange(startValue: 55, endValue: 150,color: const Color.fromARGB(255, 255, 0, 0)),
                            GaugeRange(startValue: 150, endValue: 250,color: const Color.fromARGB(255, 157, 4, 4)),
                            ],
                          pointers: <GaugePointer>[
                            NeedlePointer(
                              value: double.parse(viewModel.statsList[index]),
                              enableAnimation: true
                            )
                          ],
                          annotations: <GaugeAnnotation>[
                            GaugeAnnotation(widget:
                              Text(
                                viewModel.statsList[index],
                                style: const TextStyle(color: Color.fromARGB(255, 0, 0, 0),
                                fontSize: 32),
                              ),
                              positionFactor: 0.7,
                              angle: 90,
                            )
                          ],
                        )
                      ],
                    ),
                    ),
                    Text(
                      viewModel.labels[index],
                      style: const TextStyle(color: Color.fromARGB(255, 0, 0, 0),
                      fontSize: 12),
                    ),
                  ]
                ),
              ),
            ),
          ),
        );
        } else {
          return GridTile(
          child: Padding(padding: const EdgeInsets.all(8.0),
          child:
            Container(
              margin: const EdgeInsets.symmetric(vertical: 8.0),
              decoration: BoxDecoration(borderRadius: const BorderRadius.all(Radius.circular(8.0)),
                color: const Color.fromARGB(0, 14, 100, 129),
                border: Border.all()
              ),
              child:Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Text(
                      viewModel.statsList[index],
                      style: const TextStyle(color: Color.fromARGB(255, 0, 0, 0),
                      fontSize: 48),
                    ),
                    Text(
                      viewModel.labels[index],
                      style: const TextStyle(color: Color.fromARGB(255, 0, 0, 0),
                      fontSize: 12),
                    ),
                  ]
                ),
              ),
            ),
          ),
        );
        }
      }
    );
  }
}
