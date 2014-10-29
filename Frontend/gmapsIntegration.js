google.maps.event.addDomListener(window, 'load', initialize);

var map;
var service;
var infowindow;

var markers = [];

function initialize() {
	var pyrmont = new google.maps.LatLng(-33.8665433,151.1956316);

	map = new google.maps.Map(document.getElementById('map-canvas'), {
		center: pyrmont,
		zoom: 5
	});
}

function createMarker(dest) {
	var marker = new google.maps.Marker({
		position: dest.geometry.location,
		title: dest.formatted_address
	});

	// To add the marker to the map, call setMap();
	marker.setMap(map);
	map.setCenter(marker.position);
}


function destinationSearchCallback(results, status) {
	if (status == google.maps.places.PlacesServiceStatus.OK) {
		if(results.length > 0) {
			var destination = results[0];
			createMarker(destination);
			
			markers.push(destination);
		}
	}
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
  setAllMap(null);
}

// Shows any markers currently in the array.
function showMarkers() {
  setAllMap(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
  clearMarkers();
  markers = [];
}

// Sets the map on all markers in the array.
function setAllMap(map) {
  for (var i = 0; i < markers.length; i++) {
    markers[i].setMap(map);
  }
}

function searchDestinations() {
	deleteMarkers();

	var text = $('#destinations').val().replace( /\n/g, "___" ).split("___");
	
	for(var i = 0; i < text.length; i++) {
		if(text[i].trim() !== '') {
			var request = {
				query: text[i]
			};

			service = new google.maps.places.PlacesService(map);
			service.textSearch(request, destinationSearchCallback);
		}
	}
	
	$('#placesInput').fadeOut();
	//$('.nav .nav-sidebar
	
	return false;			// do not update UI
}

function handleLabelClick(evt) {
	var labelId = evt.target.id;
	var labelItem = $('#' + labelId);
	
	if(labelItem !== null && typeof labelItem !== 'undefined' && labelItem[0].nextElementSibling !== null && typeof labelItem[0].nextElementSibling !== 'undefined' ) {
		var blockToLabel = labelItem[0].nextElementSibling;
		
		if(blockToLabel.nodeName === "DIV" && blockToLabel.id.indexOf('inputBlock_') === 0) {
			if($(blockToLabel).is(':visible')) {
				$(blockToLabel).fadeOut();
				labelItem.removeClass('active');
			} else {
				$("[id^=inputBlock_]").fadeOut();
				$("[id^=inputBlock_]").each(function( index, element ) {
					// element == this
					var lblItemNotActive = $(element)[0].previousElementSibling;
					// remove "class active from all other classes
					if(lblItemNotActive.id.trim() !== labelId) {
						$(lblItemNotActive).removeClass('active');
					}
				  });
				$(blockToLabel).fadeIn();
				labelItem.addClass('active');
			}
		}
	}			
}

$(document).ready(function() {
	$('#btnSearchDestinations').click(searchDestinations);
	
	$('#lblEnterPlaces').click(handleLabelClick);
	$('#lblSendDataForComputation').click(handleLabelClick);			
});
