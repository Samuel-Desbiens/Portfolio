import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:url_launcher/url_launcher.dart';

import '../../data/utils/constant.dart';

//https://api.flutter.dev/flutter/material/ListTile-class.html ,  Voici le liens pour comment faire les hero transition qui sont utiliser pour faire afficher ces widgets
class DetailScreen extends StatelessWidget {
  final Ranges range;

  const DetailScreen({Key? key, required this.range}) : super(key: key);

  Future<void> _launchURL() async {
    Uri uri = Uri.parse(Constants.URL_SOURCE);
    if (await canLaunchUrl(uri)) {
      await launchUrl(uri);
    } else {
      throw LocaleKeys.app_text_wrong_link
          .tr(namedArgs: {'uri': uri.toString()});
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.black,
        title: Text(LocaleKeys.app_text_detail_screen.tr()),
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView(
              padding: const EdgeInsets.all(16.0),
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      LocaleKeys.app_text_niveau_polution.tr(),
                      style:
                          const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 10),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.start,
                      children: [
                        Hero(
                          tag: 'colorCircle_${range.label}',
                          child: Container(
                            width: 30,
                            height: 30,
                            decoration: BoxDecoration(
                              shape: BoxShape.circle,
                              color: range.getColorFromHex(),
                            ),
                          ),
                        ),
                        const SizedBox(width: 10),
                        Text(
                          range.label,
                          style: const TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                      ],
                    ),
                    const SizedBox(height: 20),
                    Text(
                      LocaleKeys.app_text_legende.tr(),
                      style:
                          const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 5),
                    Text(LocaleKeys.app_text_range_air.tr(namedArgs: {
                      'min': range.min.toString(),
                      'max': range.max.toString(),
                    })),
                    const SizedBox(height: 20),
                    Text(
                      LocaleKeys.app_text_effet_sante.tr(),
                      style:
                          const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    Text(range.healthEffect),
                    const SizedBox(height: 20),
                    Text(
                      LocaleKeys.app_text_note.tr(),
                      style:
                          const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    Text(range.note),
                  ],
                ),
              ],
            ),
          ),
          const SizedBox(height: 20),
          InkWell(
            onTap: () {
              _launchURL();
            },
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Icon(Icons.open_in_new),
                const SizedBox(width: 8),
                Text(LocaleKeys.app_text_source.tr(),
                    style: const TextStyle(fontSize: 18)),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
